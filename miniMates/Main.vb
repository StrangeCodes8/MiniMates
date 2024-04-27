Imports System.IO
Imports System.Net
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Newtonsoft.Json
Public Class Main

    Private resourcePath As String = ""
    Private rpName As String = ""
    Private User As String = ""
    Private modelData As String = ""
    Private item As String = ""

    Private resourceBaseDir As String = ""
    Private mcmetaFile_Path As String = ""
    Private itemFileDir_Path As String = ""
    Private itemFile_Path As String = ""
    Private blocksfileDir_Path As String = ""
    Private blocksFile_Path As String = ""
    Private modelFileDir_Path
    Private modelFile_Path As String = ""
    Private textureImageDir_Path As String = ""
    Private textureImage_Path As String = ""
    Private resourceItemName As String = ""

    Private Sub bExecute_Click(sender As Object, e As EventArgs) Handles bExecute.Click
        'Define resource variables and paths to make code esier to read
        resourcePath = txtResourcePack.Text
        rpName = txtRPName.Text
        User = txtUsername.Text
        modelData = txtModel.Text
        item = txtiItem.Text

        resourceBaseDir = resourcePath & "\" & rpName
        resourceItemName = rpName.ToLower & "/" & User.ToLower & "_mini"
        mcmetaFile_Path = resourceBaseDir & "\pack.mcmeta"
        textureImageDir_Path = resourceBaseDir & "\assets\minecraft\textures\" & rpName.ToLower & "\"
        textureImage_Path = textureImageDir_Path & User.ToLower & "_mini.png"
        modelFileDir_Path = resourceBaseDir & "\assets\minecraft\models\" & rpName.ToLower & "\"
        modelFile_Path = modelFileDir_Path & User.ToLower & "_mini.json"
        itemFileDir_Path = resourceBaseDir & "\assets\minecraft\models\item\"
        itemFile_Path = itemFileDir_Path & item & ".json"
        blocksfileDir_Path = resourceBaseDir & "\assets\minecraft\atlases\"
        blocksFile_Path = blocksfileDir_Path & "blocks.json"

        'Check if modifying current pack or creating new
        If Not IO.File.Exists(mcmetaFile_Path) Then
            Dim ans As MsgBoxResult = MsgBox("pack.mcmeta does not exist" & vbCrLf & vbCrLf & "Are you creating a new Resource Pack?", MsgBoxStyle.YesNo, "New Pack?")
            If ans = MsgBoxResult.Yes Then
                'Create new pack
                NewResourcePack()
            End If
        End If

        'check if skin texture image file is already downloaded, no need to ping MojangAPI if it already exists
        If Not FileIO.FileSystem.FileExists(textureImage_Path) Then
            Try
                Dim mojangAPI As MojangAPI.base = JsonConvert.DeserializeObject(Of MojangAPI.base)(getWebJson("https://api.mojang.com/users/profiles/minecraft/" & User.ToLower))
                System.Threading.Thread.Sleep(1000)
                Dim mojangProfile As MojangAPI.profile = JsonConvert.DeserializeObject(Of MojangAPI.profile)(getWebJson("https://sessionserver.mojang.com/session/minecraft/profile/" & mojangAPI.id))
                Dim mojangTexture As MojangAPI.texture = JsonConvert.DeserializeObject(Of MojangAPI.texture)(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(mojangProfile.properties(0).value)))
                If mojangTexture.textures.SKIN.url <> "" Then
                    Dim client As New System.Net.WebClient(), tempImage As String = Environment.ExpandEnvironmentVariables("%temp%") & "\" & User.ToLower & "_temp.png"
                    client.DownloadFile(mojangTexture.textures.SKIN.url, tempImage)
                    Dim base As Image = Image.FromFile(tempImage)
                    Dim stype As String = "normal"
                    If base.Height = 32 Then stype = "old"
                    If Not mojangTexture.textures.SKIN.metadata Is Nothing Then
                        If Not mojangTexture.textures.SKIN.metadata.model Is Nothing Then
                            If mojangTexture.textures.SKIN.metadata.model = "slim" Then stype = "slim"
                        End If
                    End If
                    IO.Directory.CreateDirectory(textureImageDir_Path)
                    handleTransparency(base, stype, textureImage_Path)
                Else
                    MsgBox("Error getting api information")
                End If
            Catch j As JsonException
                MsgBox("Encountered JSON Parasing error, it's possible that Microsoft updated the Mojang API and it can no longer be parsed." & vbCrLf & vbCrLf & "If this continues to happen check to see if there are updates to this program", MsgBoxStyle.Critical, "JSON Error")
            Catch u As Exception
                MsgBox("Encountered unhandled exception during execution. If this continues to happen please open a bug report and include this information" & vbCrLf & vbCrLf & u.Message)
            End Try
        Else
            'MsgBox("Skin texture file already exists no need to re-download it, continuing execution",MsgBoxStyle.Information,"Continuing")
        End If
        If FileIO.FileSystem.FileExists(textureImage_Path) Then
            If Not FileIO.FileSystem.FileExists(itemFile_Path) Then FileIO.FileSystem.WriteAllText(itemFile_Path, My.Resources.emptyItemJson, False, System.Text.Encoding.UTF8)
            Dim itmFile As Item.Item = JsonConvert.DeserializeObject(Of Item.Item)(FileIO.FileSystem.ReadAllText(itemFile_Path))
            If Not FileIO.FileSystem.FileExists(blocksFile_Path) Then FileIO.FileSystem.WriteAllText(blocksFile_Path, My.Resources.emptyblockjson, False, System.Text.Encoding.UTF8)
            Dim blocksFile As blocks.blocks = JsonConvert.DeserializeObject(Of blocks.blocks)(FileIO.FileSystem.ReadAllText(blocksFile_Path))
            Dim newItem As New Item.ovrrides(rpName.ToLower & "/" & User.ToLower & "_mini", modelData)
            Dim ret As Boolean = itmFile.addOverride(newItem)
            If ret Then
                FileIO.FileSystem.WriteAllText(itemFile_Path, JsonConvert.SerializeObject(itmFile, Formatting.None), False, System.Text.Encoding.UTF8)
                Dim newTexture As New blocks.sources("minecraft:" & resourceItemName, "minecraft:" & resourceItemName, "single")
                If blocksFile.Check(newTexture) Then
                    'MsgBox("atlas texture map already exists, no need to add it again",MsgBoxStyle.Information,"Continuing")
                Else
                    blocksFile.sources.Add(newTexture)
                    FileIO.FileSystem.WriteAllText(blocksFile_Path, JsonConvert.SerializeObject(blocksFile, Formatting.None), False, System.Text.Encoding.UTF8)
                    MsgBox("Everything appears to have completed succesfully, time to test out your new item. Make sure that your new resource pack is selected and loaded (reload if already in use F3 + T) and use this command to give yourself the new item" _
                            & vbCrLf & vbCrLf & "/give @p minecraft:" & item & "{CustomModelData:" & modelData & "}", MsgBoxStyle.Information, "Success")
                End If
            End If
        Else
            MsgBox("Something happened while attempting to download the skin texture for " & User & vbCrLf & vbCrLf & "Can not continue, please try again", MsgBoxStyle.Critical, "Error")
        End If
    End Sub

    Private Sub NewResourcePack()
        FileIO.FileSystem.CreateDirectory(itemFileDir_Path)
        FileIO.FileSystem.CreateDirectory(modelFileDir_Path)
        FileIO.FileSystem.CreateDirectory(blocksfileDir_Path)
        FileIO.FileSystem.WriteAllText(itemFile_Path, My.Resources.emptyItemJson, False, System.Text.Encoding.UTF8)
        FileIO.FileSystem.WriteAllText(blocksFile_Path, My.Resources.emptyblockjson, False, System.Text.Encoding.UTF8)
        FileIO.FileSystem.WriteAllText(mcmetaFile_Path, My.Resources.emptyPackmcdata.Replace("%packName%", rpName), False, System.Text.Encoding.UTF8)
    End Sub

    Private Function getWebJson(ByVal url As String)

        Dim webRequest As WebRequest = WebRequest.Create(url)
        Dim request As HttpWebRequest = CType(webRequest, HttpWebRequest)
        request.Method = "GET"
        request.ContentType = "application/json"

        Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
        Select Case response.StatusCode
            Case HttpStatusCode.OK
                Using reader As StreamReader = New StreamReader(response.GetResponseStream())
                    Return reader.ReadToEnd()
                End Using
            Case HttpStatusCode.NotFound
                MsgBox("Mojang API returned 404. Doublecheck your internet connection and if it is good it's possible that Mojang API was discontinued", MsgBoxStyle.Critical, "Page not Found")
            Case 429
                MsgBox("Mojang API has throttled your connection, please wait a little bit before trying again", MsgBoxStyle.Exclamation, "Too Many Requests")
            Case Else
                MsgBox("Mojang API returned an otherwise unhandled return code: " & response.StatusCode & vbCrLf & vbCrLf & "If this continues to happne please open a bugreport", MsgBoxStyle.Critical, "Unhandled Return")
        End Select
        Return response.StatusCode
    End Function
    Private Sub handleTransparency(ByRef base As Image, ByVal type As String, ByVal dest As String)
        Select Case type
            Case "normal"
                Dim blackLayer As Image = My.Resources.blacklayer
                Dim destImage As Bitmap = New Bitmap(64, 64)
                Dim graphics As Graphics = Graphics.FromImage(destImage)
                graphics.DrawImage(blackLayer, 0, 0, 64, 64)
                graphics.DrawImage(base, 0, 0, 64, 64)
                graphics.Dispose()
                blackLayer.Dispose()
                base.Dispose()
                destImage.Save(dest)
                FileIO.FileSystem.WriteAllText(modelFile_Path, My.Resources.baseMini.ToString().Replace("texturePath", resourceItemName), False, System.Text.Encoding.UTF8)
            Case "slim"
                Dim blackLayer As Image = My.Resources.blacklayer_slim
                Dim destImage As Bitmap = New Bitmap(64, 64)
                Dim graphics As Graphics = Graphics.FromImage(destImage)
                graphics.DrawImage(blackLayer, 0, 0, 64, 64)
                graphics.DrawImage(base, 0, 0, 64, 64)
                graphics.Dispose()
                blackLayer.Dispose()
                base.Dispose()
                destImage.Save(dest)
                FileIO.FileSystem.WriteAllText(modelFile_Path, My.Resources.baseMini_slim.ToString().Replace("texturePath", resourceItemName), False, System.Text.Encoding.UTF8)
            Case "old"
                Dim blackLayer As Image = My.Resources.blacklayer_old
                Dim destImage As Bitmap = New Bitmap(64, 32)
                Dim graphics As Graphics = Graphics.FromImage(destImage)
                graphics.DrawImage(blackLayer, 0, 0, 64, 32)
                graphics.DrawImage(base, 0, 0, 64, 32)
                graphics.Dispose()
                blackLayer.Dispose()
                base.Dispose()
                destImage.Save(dest)
                FileIO.FileSystem.WriteAllText(modelFile_Path, My.Resources.baseMini_oldSkin.ToString().Replace("texturePath", resourceItemName), False, System.Text.Encoding.UTF8)
        End Select

    End Sub

    Private Sub bBrowse_Click(sender As Object, e As EventArgs) Handles bBrowse.Click
        Dim Path As String = ""
        Dim fbd As New FolderBrowserDialog

        Path = Environment.ExpandEnvironmentVariables("%appdata%") & "\.minecraft\resourcepacks"

        fbd.SelectedPath = Path
        If fbd.ShowDialog() = DialogResult.OK Then
            txtResourcePack.Text = fbd.SelectedPath
        End If
    End Sub
End Class
