Public Class MojangAPI
    Public Class base
        Public id As String
        Public name As String
    End Class
    Public Class profile
        Public id As String
        Public name As String
        Public properties As List(Of properties)
        Public profileActions As List(Of String)

    End Class
    Public Class properties
        Public name As String
        Public value As String
    End Class
    Public Class texture
        Public timestamp As Long
        Public profileId As String
        Public profileName As String
        Public signatureRequired As Boolean
        Public textures As skins
    End Class
    Public Class skins
        Public SKIN As skin
    End Class
    Public Class skin
        Public url As String
        Public metadata As metadata
    End Class
    Public Class metadata
        Public model As String
    End Class
End Class
