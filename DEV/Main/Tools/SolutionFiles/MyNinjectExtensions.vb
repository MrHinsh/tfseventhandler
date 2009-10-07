Imports Ninject.Core
Imports Hinshlabs.Wpf

Namespace My

    <HideModuleName()> _
    Module MyNinjectExtensions

        Friend ReadOnly Property Kernel() As IKernel
            Get
                Return NinjectFactory.Instance.Kernel
            End Get
        End Property

    End Module

End Namespace