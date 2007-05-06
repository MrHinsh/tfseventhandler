Imports System
Imports Microsoft.VisualStudio.CommandBars
Imports Extensibility
Imports EnvDTE
Imports EnvDTE80

Public Class Connect
	
    Implements IDTExtensibility2
	Implements IDTCommandTarget

    Dim _applicationObject As DTE2
    Dim _addInInstance As AddIn
    Dim _windowToolWindow As Window

    '''<summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>
    Public Sub New()

    End Sub

    '''<summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
    '''<param name='application'>Root object of the host application.</param>
    '''<param name='connectMode'>Describes how the Add-in is being loaded.</param>
    '''<param name='addInInst'>Object representing this Add-in.</param>
    '''<remarks></remarks>
    Public Sub OnConnection(ByVal application As Object, ByVal connectMode As ext_ConnectMode, ByVal addInInst As Object, ByRef custom As Array) Implements IDTExtensibility2.OnConnection
        _applicationObject = CType(application, DTE2)
        _addInInstance = CType(addInInst, AddIn)
		If connectMode = ext_ConnectMode.ext_cm_UISetup Then

            Dim commands As Commands2 = CType(_applicationObject.Commands, Commands2)
            Dim toolsMenuName As String
            Try

                'If you would like to move the command to a different menu, change the word "Tools" to the 
                '  English version of the menu. This code will take the culture, append on the name of the menu
                '  then add the command to that menu. You can find a list of all the top-level menus in the file
                '  CommandBar.resx.
                Dim resourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("RDDotNet.TeamFoundation.EventHandler.CommandBar", System.Reflection.Assembly.GetExecutingAssembly())

                Dim cultureInfo As System.Globalization.CultureInfo = New System.Globalization.CultureInfo(_applicationObject.LocaleID)
                toolsMenuName = resourceManager.GetString(String.Concat(cultureInfo.TwoLetterISOLanguageName, "Tools"))

            Catch e As Exception
                'We tried to find a localized version of the word Tools, but one was not found.
                '  Default to the en-US word, which may work for the current culture.
                toolsMenuName = "Tools"
            End Try

            'Place the command on the tools menu.
            'Find the MenuBar command bar, which is the top-level command bar holding all the main menu items:
            Dim commandBars As CommandBars = CType(_applicationObject.CommandBars, CommandBars)
            Dim menuBarCommandBar As CommandBar = commandBars.Item("MenuBar")

            'Find the Tools command bar on the MenuBar command bar:
            Dim toolsControl As CommandBarControl = menuBarCommandBar.Controls.Item(toolsMenuName)
            Dim toolsPopup As CommandBarPopup = CType(toolsControl, CommandBarPopup)

            Try
                'Add a command to the Commands collection:
                Dim command As Command = commands.AddNamedCommand2(_addInInstance, "RDDotNet.TeamFoundation.EventHandler", "RDDotNet.TeamFoundation.EventHandler", "Executes the command for RDDotNet.TeamFoundation.EventHandler", True, 59, Nothing, CType(vsCommandStatus.vsCommandStatusSupported, Integer) + CType(vsCommandStatus.vsCommandStatusEnabled, Integer), vsCommandStyle.vsCommandStylePictAndText, vsCommandControlType.vsCommandControlTypeButton)

                'Find the appropriate command bar on the MenuBar command bar:
                command.AddControl(toolsPopup.CommandBar, 1)
            Catch argumentException As System.ArgumentException
                'If we are here, then the exception is probably because a command with that name
                '  already exists. If so there is no need to recreate the command and we can 
                '  safely ignore the exception.
            End Try
        End If

        '----------------------------------------------------------
        Dim programmableObject As Object

        'This guid must be unique for each different tool window,
        ' but you may use the same guid for the same tool window.
        'This guid can be used for indexing the windows collection,
        ' for example: applicationObject.Windows.Item(guidstr)
        Dim guidstr As String = "{439bc5e4-6ec1-491f-adc9-fffe36776c2e}"
        Dim windows2 As EnvDTE80.Windows2
        Dim asm As System.Reflection.Assembly
        asm = System.Reflection.Assembly.GetExecutingAssembly()
        windows2 = CType(_applicationObject.Windows, EnvDTE80.Windows2)
        _windowToolWindow = windows2.CreateToolWindow2(_addInInstance, asm.Location, "RDDotNet.TeamFoundation.EventHandler.TFSEventH%andlerControl", "TFS Event Handler Explorer", guidstr, programmableObject)

        'Set the picture displayed when the window is tab docked
        '_windowToolWindow.SetTabPicture(My.Resources.Resource.ToolWindowBmp.GetHbitmap())

        'When using the hosting control, you must set visible to true before calling HostUserControl,
        ' otherwise the UserControl cannot be hosted properly.
        _windowToolWindow.Visible = True
        MsgBox("DONE")
        '----------------------------------------------------------
	End Sub

    '''<summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
    '''<param name='disconnectMode'>Describes how the Add-in is being unloaded.</param>
    '''<param name='custom'>Array of parameters that are host application specific.</param>
    '''<remarks></remarks>
    Public Sub OnDisconnection(ByVal disconnectMode As ext_DisconnectMode, ByRef custom As Array) Implements IDTExtensibility2.OnDisconnection
    End Sub

    '''<summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification that the collection of Add-ins has changed.</summary>
    '''<param name='custom'>Array of parameters that are host application specific.</param>
    '''<remarks></remarks>
    Public Sub OnAddInsUpdate(ByRef custom As Array) Implements IDTExtensibility2.OnAddInsUpdate
    End Sub

    '''<summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
    '''<param name='custom'>Array of parameters that are host application specific.</param>
    '''<remarks></remarks>
    Public Sub OnStartupComplete(ByRef custom As Array) Implements IDTExtensibility2.OnStartupComplete
    End Sub

    '''<summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
    '''<param name='custom'>Array of parameters that are host application specific.</param>
    '''<remarks></remarks>
    Public Sub OnBeginShutdown(ByRef custom As Array) Implements IDTExtensibility2.OnBeginShutdown
    End Sub
	
    '''<summary>Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's availability is updated</summary>
    '''<param name='commandName'>The name of the command to determine state for.</param>
    '''<param name='neededText'>Text that is needed for the command.</param>
    '''<param name='status'>The state of the command in the user interface.</param>
    '''<param name='commandText'>Text requested by the neededText parameter.</param>
    '''<remarks></remarks>
    Public Sub QueryStatus(ByVal commandName As String, ByVal neededText As vsCommandStatusTextWanted, ByRef status As vsCommandStatus, ByRef commandText As Object) Implements IDTCommandTarget.QueryStatus
        If neededText = vsCommandStatusTextWanted.vsCommandStatusTextWantedNone Then
            If commandName = "RDDotNet.TeamFoundation.EventHandler.Connect.RDDotNet.TeamFoundation.EventHandler" Then
                status = CType(vsCommandStatus.vsCommandStatusEnabled + vsCommandStatus.vsCommandStatusSupported, vsCommandStatus)
            Else
                status = vsCommandStatus.vsCommandStatusUnsupported
            End If
        End If
    End Sub

    '''<summary>Implements the Exec method of the IDTCommandTarget interface. This is called when the command is invoked.</summary>
    '''<param name='commandName'>The name of the command to execute.</param>
    '''<param name='executeOption'>Describes how the command should be run.</param>
    '''<param name='varIn'>Parameters passed from the caller to the command handler.</param>
    '''<param name='varOut'>Parameters passed from the command handler to the caller.</param>
    '''<param name='handled'>Informs the caller if the command was handled or not.</param>
    '''<remarks></remarks>
    Public Sub Exec(ByVal commandName As String, ByVal executeOption As vsCommandExecOption, ByRef varIn As Object, ByRef varOut As Object, ByRef handled As Boolean) Implements IDTCommandTarget.Exec
        handled = False
        If executeOption = vsCommandExecOption.vsCommandExecOptionDoDefault Then
            If commandName = "RDDotNet.TeamFoundation.EventHandler.Connect.RDDotNet.TeamFoundation.EventHandler" Then
                handled = True
                Exit Sub
            End If
        End If
	End Sub
End Class
