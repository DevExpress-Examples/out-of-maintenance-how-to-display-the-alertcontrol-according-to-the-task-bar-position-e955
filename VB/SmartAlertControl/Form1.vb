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
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
			Dim info As New AlertInfo("DX Sample", "This alert takes into account the taskbar position")
			AlertHelper.ShowAlertNearTaskBar(alertControl1, Me, info)
		End Sub
	End Class
End Namespace