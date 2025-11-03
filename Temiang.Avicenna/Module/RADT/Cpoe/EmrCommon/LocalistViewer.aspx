<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="LocalistViewer.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.LocalistViewer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadImageGallery RenderMode="Lightweight" ID="imgGal" runat="server" OnNeedDataSource="imgGal_OnNeedDataSource" DataDescriptionField="Notes" DataImageField="BodyImage"
                             DataTitleField="BodyID" Width="600px" Height="480px" LoopItems="true">
        <ThumbnailsAreaSettings ThumbnailWidth="100px" ThumbnailHeight="80px" Height="80px" />
        <ImageAreaSettings Height="400px" />
    </telerik:RadImageGallery>

</asp:Content>
