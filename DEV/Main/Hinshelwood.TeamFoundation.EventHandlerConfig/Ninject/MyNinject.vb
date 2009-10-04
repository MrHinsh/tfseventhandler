Imports Ninject.Core

Namespace My

    <HideModuleName()> _
    Public Module MyNinject

        Private m_kernal As IKernel

        Public ReadOnly Property Kernel() As IKernel
            Get
                If m_kernal Is Nothing Then
                    m_kernal = GetKernal()
                End If
                Return m_kernal
            End Get
        End Property

        Public Function GetKernal() As IKernel
            Dim kernel As New StandardKernel



            Return kernel
        End Function

    End Module

End Namespace