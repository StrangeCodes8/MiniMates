$xaml = @'
<Window
   xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
   xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
   MinWidth="200"
   Width ="400"
   SizeToContent="Height"
   Title="MinMates"
   Topmost="True" Height="337.325">
    <Grid Margin="10,40,10,10">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto" MinWidth="78"/>
            <ColumnDefinition/>
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>
        <TextBlock Grid.Column="0" Grid.Row="0" Grid.ColumnSpan="2" Margin="5">Fill out the info below to create a mini:</TextBlock>

        <TextBlock Grid.Column="0" Grid.Row="1" Margin="5">Path to ResourcePack</TextBlock>
        <TextBlock Grid.Column="0" Grid.Row="2" Margin="5">Player Username</TextBlock>
        <TextBlock Grid.Column="0" Grid.Row="3" Margin="5">custom_model_data</TextBlock>
        <TextBlock Grid.Column="0" Grid.Row="4" Margin="5">Item to be used</TextBlock>
        <TextBox Name="txtResourcePack" Grid.Column="1" Grid.Row="1" Margin="5"/>
        <TextBox Name="txtUser" Grid.Column="1" Grid.Row="2" Margin="5"/>
        <TextBox Name="txtModelData" Grid.Column="1" Grid.Row="3" Margin="5"/>
        <TextBox Name="txtItem" Grid.Column="1" Grid.Row="4" Margin="5"/>

        <StackPanel Orientation="Horizontal" HorizontalAlignment="Right" VerticalAlignment="Bottom" Grid.Row="5" Grid.Column="1" Height="32" Width="180">
            <Button Name="ButOk" MinWidth="80" Height="22" Margin="5">Execute</Button>
            <Button Name="ButCancel" MinWidth="80" Height="22" Margin="5">Cancel</Button>
        </StackPanel>
    </Grid>
</Window>
'@
#endregion

#region Code Behind
function Convert-XAMLtoWindow
{
  param
  (
    [Parameter(Mandatory=$true)]
    [string]
    $XAML
  )
  
  Add-Type -AssemblyName PresentationFramework
  
  $reader = [XML.XMLReader]::Create([IO.StringReader]$XAML)
  $result = [Windows.Markup.XAMLReader]::Load($reader)
  $reader.Close()
  $reader = [XML.XMLReader]::Create([IO.StringReader]$XAML)
  while ($reader.Read())
  {
    $name=$reader.GetAttribute('Name')
    if (!$name) { $name=$reader.GetAttribute('x:Name') }
    if($name)
    {$result | Add-Member NoteProperty -Name $name -Value $result.FindName($name) -Force}
  }
  $reader.Close()
  $result
}

function Show-WPFWindow
{
  param
  (
    [Parameter(Mandatory=$true)]
    [Windows.Window]
    $Window
  )
  
  $result = $null
  $null = $window.Dispatcher.InvokeAsync{
    $result = $window.ShowDialog()
    Set-Variable -Name result -Value $result -Scope 1
  }.Wait()
  $result
}
#endregion Code Behind

#region Convert XAML to Window
$window = Convert-XAMLtoWindow -XAML $xaml 
#endregion

$basePath = $(split-path -parent $MyInvocation.MyCommand.Definition)

#region Manipulate Window Content
$window.txtUser.Text = "notch"
$window.txtModelData.Text = '100'
$window.txtResourcePack.text = "%appdata%\.minecraft\resourcepacks\miniMates"
$window.txtItem.text = "paper"
$null = $window.txtUser.Focus()
#endregion

#Load RP Files

function insert-intoitem {
  Param(
    [Parameter(Mandatory = $true)]
    [ValidateNotNullOrEmpty()]
    $JsonData,
    [Parameter(Mandatory = $true)]
    [ValidateNotNullOrEmpty()]
    $newObject        
  )
  [System.Collections.ArrayList]$copy = $JsonData
  [int32]$index = 0
   ForEach ($itm in $copy)  {
    if ($itm.model -eq $newObject.model){
      Write-Host "Item texture already exists" -ForegroundColor Red
      return "found"
    }
    if ($itm.predicate.custom_model_data -eq $newObject.predicate.custom_model_data){
      write-host "Custom ID already exists" -ForegroundColor Red
      return "found"
    }
    if ($newObject.predicate.custom_model_data -gt $itm.predicate.custom_model_data){
      
    } else {
      $index = $copy.IndexOf($itm)
      break
    }
  }    
  $copy.Insert($index,$newObject)
  return ($copy)
}

###Add new skin texture to item of choice
function add-NewMini { 
  $itemFile = Get-Content "$($window.txtResourcePack.text)\assets\minecraft\models\item\$($window.txtItem.Text).json" | ConvertFrom-Json 
  $atlasFile = Get-Content "$($window.txtResourcePack.text)\assets\minecraft\atlases\blocks.json" | ConvertFrom-Json
  $Utf8NoBomEncoding = New-Object System.Text.UTF8Encoding $False
  $newItem = @"
{
  "model": "minimates/$($window.txtUser.text)_mini",
  "predicate": {
    "custom_model_data": $($window.txtModelData.text)
  }
}
"@ | Convertfrom-Json
  
  $ret = insert-intoitem -JsonData $itemFile.overrides -newObject $newItem
  if ($ret -ne "found"){
    $itemFile.overrides = $ret
    [System.IO.File]::WriteAllLines("$($window.txtResourcePack.text)\assets\minecraft\models\item\$($window.txtItem.Text).json", ($itemFile | ConvertTo-Json -Depth 100 -Compress), $Utf8NoBomEncoding)


    ###Get Texture file
    #Get API
    $mojAPI = ((Invoke-WebRequest "https://api.ashcon.app/mojang/v2/user/$($window.txtUser.Text)") | ConvertFrom-Json)
    $skinURL = $mojapi.textures.SKIN.url
    #Create directory if not exists
    New-Item -Path "$($window.txtResourcePack.text)\assets\minecraft\textures\minimates" -ItemType Directory -ErrorAction SilentlyContinue
    #Checkfor and download skin texture
    if (!(Get-Item "$($window.txtResourcePack.text)\assets\minecraft\textures\minimates\$($window.txtUser.text)_mind.png" -ErrorAction SilentlyContinue)){
      Invoke-WebRequest $skinURL -OutFile "$($window.txtResourcePack.text)\assets\minecraft\textures\minimates\$($window.txtUser.text)_mini.png"
    }
    Add-Type -AssemblyName System.Drawing
    #check for existing model
    if (!(Get-Item "$($window.txtResourcePack.text)\assets\minecraft\models\minimates\$($window.txtUser.text)_mini.json")){
      #get texture diminisions (to check for old skins)
      $image = [System.Drawing.Image]::FromFile((Get-Item "$($window.txtResourcePack.text)\assets\minecraft\textures\minimates\$($window.txtUser.text)_mini.png"))    
      if ($image.Height -eq 32){    
        $baseTextureFile = Get-Content "$basePath\baseMini_oldSkin.json" | ConvertFrom-Json
      } elseif ($mojAPI.textures.slim) {
        $baseTextureFile = Get-Content "$basePath\baseMini_slim.json" | ConvertFrom-Json
      } else { 
        $baseTextureFile = Get-Content "$basePath\baseMini.json" | ConvertFrom-Json
      }
      ###Modify baseModel
      $baseTextureFile.textures.0 = "minimates/$($window.txtUser.text)_mini"
      $baseTextureFile.textures.particle = "minimates/$($window.txtUser.text)_mini"
      New-Item -Path "$($window.txtResourcePack.text)\assets\minecraft\models\minimates" -ItemType Directory -ErrorAction SilentlyContinue
  
      [System.IO.File]::WriteAllLines("$($window.txtResourcePack.text)\assets\minecraft\models\minimates\$($window.txtUser.text)_mini.json",($baseTextureFile | ConvertTo-Json -Depth 100 -Compress ),$Utf8NoBomEncoding)
      Write-Host "Item custom data has been added" -ForegroundColor Green
      
    } else {
      Write-Host "Item custom data already exists" -ForegroundColor Cyan
    }
    #Modify Atlas File
    $newTextureSource = @"
{
  "resource": "minecraft:minimates/$($window.txtUser.text)_mini",
  "sprite": "minecraft:minimates/$($window.txtUser.text)_mini",
  "type": "single"
}
"@ | ConvertFrom-Json
  
    [bool]$exists = $False
    foreach($itm in $atlasFile.sources){
      if($itm.resource -eq $newTextureSource.resource){
        $exists = $true
        Write-Host "Texture mapping already exists" -ForegroundColor Cyan
        break
      }
    }
    if (!$exists){
      $atlasFile.sources += $newTextureSource  
      [System.IO.File]::WriteAllLines("$($window.txtResourcePack.text)\assets\minecraft\atlases\blocks.json",($atlasFile | ConvertTo-Json -Depth 100 -Compress),$Utf8NoBomEncoding)
      Write-Host "Texture mapping added" -ForegroundColor Green
    }
  }
}

$window.ButCancel.add_Click(
  {
    $window.DialogResult = $false
  }
)

$window.ButOk.add_Click(
  {
    $window.txtUser.text = $window.txtUser.text.ToLower()
    add-NewMini
  }
)

# Show Window
$result = Show-WPFWindow -Window $window



#region Process results
if ($result -eq $true)
{
  
}
else
{
  Write-Warning 'User aborted dialog.'
}
#endregion Process results

