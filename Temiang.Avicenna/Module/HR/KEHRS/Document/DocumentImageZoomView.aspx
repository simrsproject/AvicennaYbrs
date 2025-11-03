<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="DocumentImageZoomView.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.KEHRS.Document.DocumentImageZoomView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <img id='wheelzoom' src='ImageHandler.ashx?id=<%=PatientDocumentID %>' style='width: auto; height: auto; cursor: pointer;' alt=''>
    <script type="text/ecmascript" src="../../../../../JavaScript/wheelzoom.js"></script>
    <script type="text/ecmascript">
        wheelzoom(document.querySelector('#wheelzoom'), { zoom: 0.1 });
    </script>--%>

    <telerik:RadImageGallery RenderMode="Lightweight" ID="imgGallery" runat="server" OnNeedDataSource="imgGallery_NeedDataSource"
        DataImageField="ImageUrl" DataDescriptionField="Notes"
        DataTitleField="DocumentName" Width="100%" LoopItems="false">
        <ImageAreaSettings ResizeMode="Fit" />
        <ThumbnailsAreaSettings ThumbnailWidth="120px" ThumbnailHeight="80px" Height="80px" />
    </telerik:RadImageGallery>

        <script type="text/javascript" language="javascript">
        function applyGridHeightMax() {
            var height =
                (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

            // set height to the whole RadGrid control
            var grid = $find("<%= imgGallery.ClientID %>");
            grid.get_element().style.height = height - 40 + "px";
            grid.repaint();
        }
        window.onload = function () {
            applyGridHeightMax();
        }
        window.onresize = function () {
            applyGridHeightMax();
        }

        // After postback
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            applyGridHeightMax();
        });
        </script>
</asp:Content>
