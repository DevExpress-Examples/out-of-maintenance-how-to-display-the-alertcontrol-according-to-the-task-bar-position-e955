Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraBars.Alerter
Imports System.Runtime.InteropServices

Namespace SmartAlertControl
	Public NotInheritable Class AlertHelper

		Private Sub New()
		End Sub
		Private Shared Function GetTaskbarLocation() As AlertFormLocation
			Dim tbwnd As IntPtr = FindWindow("Shell_TrayWnd", Nothing)
            If tbwnd = Nothing Then
                Return AlertFormLocation.BottomRight
            End If
			Dim abd As New AppBarData()
			abd.cbSize = Marshal.SizeOf(GetType(AppBarData))
			abd.hWnd = tbwnd
			Dim ptr As IntPtr = Marshal.AllocHGlobal(abd.cbSize)
			Try
				Marshal.StructureToPtr(abd, ptr, False)
				SHAppBarMessage(5, ptr)
				abd = CType(Marshal.PtrToStructure(ptr, GetType(AppBarData)), AppBarData)
			Finally
				Marshal.FreeHGlobal(ptr)
			End Try
			Return GetPosition(abd.rc)
		End Function

		Private Shared Function GetPosition(ByVal rect As Rect) As AlertFormLocation
			If rect.top = rect.left AndAlso rect.bottom > rect.right Then
				Return AlertFormLocation.BottomLeft
			End If
			If rect.top = rect.left AndAlso rect.bottom < rect.right Then
				Return AlertFormLocation.TopRight
			End If
			Return AlertFormLocation.BottomRight
		End Function

		Public Shared Sub ShowAlertNearTaskBar(ByVal alertControl As AlertControl, ByVal parent As Form, ByVal info As AlertInfo)
			alertControl.FormLocation = GetTaskbarLocation()
			alertControl.Show(parent, info)
		End Sub

		<DllImport("user32.dll")> _
		Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
		End Function

		<DllImport("shell32.dll")> _
		Shared Function SHAppBarMessage(ByVal dwMessage As UInteger, ByVal pAppBarData As IntPtr) As UIntPtr
		End Function

		<StructLayout(LayoutKind.Sequential)> _
		Private Structure AppBarData
			Public cbSize As Integer
			Public hWnd As IntPtr
			Public uCallbackMessage As UInteger
			Public uEdge As UInteger
			Public rc As Rect
			Public lParam As IntPtr
		End Structure

		<StructLayout(LayoutKind.Sequential)> _
		Private Structure Rect
			Public left As Integer
			Public top As Integer
			Public right As Integer
			Public bottom As Integer
		End Structure
	End Class
End Namespace
