<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="PersonalDocumentImageZoomView2.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.PersonalDocumentImageZoomView2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadImageEditor RenderMode="Lightweight" ID="RadImageEditor1" runat="server"
        OnImageLoading="RadImageEditor1_ImageLoading" Width="790px" Height="430px">
        <Tools>
            <telerik:ImageEditorToolGroup>
                <telerik:ImageEditorTool CommandName="RotateRight" ToolTip="Rotate Right by 90 degrees"></telerik:ImageEditorTool>
                <telerik:ImageEditorTool CommandName="RotateLeft" ToolTip="Rotate Left by 90 degrees"></telerik:ImageEditorTool>
            </telerik:ImageEditorToolGroup>
        </Tools>
    </telerik:RadImageEditor>
</asp:Content>
