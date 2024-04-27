Imports Newtonsoft.Json
Public Class Item
    Public Class Item
        <JsonProperty("overrides")>
        Public ovrrides As List(Of ovrrides)
        Public parent As String
        Public textures As textures
        Public Function addOverride(ByVal o As ovrrides) As Boolean
            If ovrrides.Count = 0 Then
                ovrrides.Add(o)
                Return True
            Else
                Dim idx As Integer = 0, found As Boolean = False
                For Each itm As ovrrides In ovrrides
                    If o.predicate.custom_model_data = itm.predicate.custom_model_data Then
                        MsgBox("custom_model_data " & o.predicate.custom_model_data & " already exists for " & itm.model, MsgBoxStyle.Critical, "Error")
                        found = True
                        Return False
                    ElseIf o.model = itm.model Then
                        MsgBox("Model " & o.model & " already exists for custom_model_data " & itm.predicate.custom_model_data)
                        found = True
                        Return False
                    ElseIf o.predicate.custom_model_data > itm.predicate.custom_model_data Or (o.predicate.custom_model_data < itm.predicate.custom_model_data And ovrrides.IndexOf(itm) = 0) Then
                        idx = ovrrides.IndexOf(itm)
                    End If
                Next
                If Not found Then
                    ovrrides.Insert(idx + 1, o)
                    Return True
                End If
            End If
            Return False
        End Function
    End Class
    Public Class ovrrides
        Public model As String
        Public predicate As predicate
        Public Sub New(m As String, p As String)
            model = m
            predicate = New predicate(p)
        End Sub
    End Class
    Public Class textures
        Public layer0 As String
    End Class
    Public Class predicate
        Public custom_model_data As Integer
        Public Sub New(p As String)
            custom_model_data = p
        End Sub
    End Class
End Class
