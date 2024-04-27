<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtResourcePack = New System.Windows.Forms.TextBox()
        Me.txtRPName = New System.Windows.Forms.TextBox()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.txtModel = New System.Windows.Forms.TextBox()
        Me.bBrowse = New System.Windows.Forms.Button()
        Me.bExecute = New System.Windows.Forms.Button()
        Me.bCancel = New System.Windows.Forms.Button()
        Me.txtiItem = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(184, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Fill out the form below to create a mini"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Path to ResourcePack"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(49, 117)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Player Username"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(34, 150)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "custom_model_data"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(40, 183)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Item to be Skinned"
        '
        'txtResourcePack
        '
        Me.txtResourcePack.Location = New System.Drawing.Point(143, 50)
        Me.txtResourcePack.Name = "txtResourcePack"
        Me.txtResourcePack.Size = New System.Drawing.Size(276, 20)
        Me.txtResourcePack.TabIndex = 5
        '
        'txtRPName
        '
        Me.txtRPName.Location = New System.Drawing.Point(142, 82)
        Me.txtRPName.Name = "txtRPName"
        Me.txtRPName.Size = New System.Drawing.Size(276, 20)
        Me.txtRPName.TabIndex = 6
        Me.txtRPName.Text = "miniMates"
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(142, 114)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(276, 20)
        Me.txtUsername.TabIndex = 7
        '
        'txtModel
        '
        Me.txtModel.Location = New System.Drawing.Point(142, 146)
        Me.txtModel.Name = "txtModel"
        Me.txtModel.Size = New System.Drawing.Size(276, 20)
        Me.txtModel.TabIndex = 8
        '
        'bBrowse
        '
        Me.bBrowse.Location = New System.Drawing.Point(426, 46)
        Me.bBrowse.Name = "bBrowse"
        Me.bBrowse.Size = New System.Drawing.Size(75, 23)
        Me.bBrowse.TabIndex = 9
        Me.bBrowse.Text = "Browse"
        Me.bBrowse.UseVisualStyleBackColor = True
        '
        'bExecute
        '
        Me.bExecute.Location = New System.Drawing.Point(243, 206)
        Me.bExecute.Name = "bExecute"
        Me.bExecute.Size = New System.Drawing.Size(75, 23)
        Me.bExecute.TabIndex = 10
        Me.bExecute.Text = "Execute"
        Me.bExecute.UseVisualStyleBackColor = True
        '
        'bCancel
        '
        Me.bCancel.Location = New System.Drawing.Point(336, 206)
        Me.bCancel.Name = "bCancel"
        Me.bCancel.Size = New System.Drawing.Size(75, 23)
        Me.bCancel.TabIndex = 11
        Me.bCancel.Text = "Cancel"
        Me.bCancel.UseVisualStyleBackColor = True
        '
        'txtiItem
        '
        Me.txtiItem.Location = New System.Drawing.Point(142, 180)
        Me.txtiItem.Name = "txtiItem"
        Me.txtiItem.Size = New System.Drawing.Size(276, 20)
        Me.txtiItem.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(18, 85)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(112, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Resource Pack Name"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(559, 255)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtiItem)
        Me.Controls.Add(Me.bCancel)
        Me.Controls.Add(Me.bExecute)
        Me.Controls.Add(Me.bBrowse)
        Me.Controls.Add(Me.txtModel)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.txtRPName)
        Me.Controls.Add(Me.txtResourcePack)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Main"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtResourcePack As TextBox
    Friend WithEvents txtRPName As TextBox
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtModel As TextBox
    Friend WithEvents bBrowse As Button
    Friend WithEvents bExecute As Button
    Friend WithEvents bCancel As Button
    Friend WithEvents txtiItem As TextBox
    Friend WithEvents Label6 As Label
End Class
