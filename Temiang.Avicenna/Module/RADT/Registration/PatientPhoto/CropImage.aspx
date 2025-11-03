<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="CropImage.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.CropImage"
    Title="Crop Picture" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        /* Limit the background image by adding overflow: hidden */
        #image-cropper
        {
            overflow: hidden;
        }
        .cropit-image-preview
        {
            background-color: #f8f8f8;
            background-size: cover;
            border: 5px solid #ccc;
            border-radius: 3px;
            margin-top: 7px;
            width: 240px;
            height: 240px;
            cursor: move;
        }
        /* Translucent background image */.cropit-image-background
        {
            opacity: .2;
            cursor: auto;
        }
        .image-size-label
        {
            margin-top: 10px;
        }
        /*
         * If the slider or anything else is covered by the background image,
         * use relative or absolute position on it
         */input.cropit-image-zoom-input
        {
            position: relative;
            width: 240px;
        }
    </style>

    <script src="Plugin/jquery-1.8.3.min.js" type="text/javascript"></script>

    <script src="Plugin/jquery.cropit.js" type="text/javascript"></script>

    <div id="image-cropper" style="position: absolute; left: 50%; margin-left: -125px;">
        <!-- .cropit-image-preview-container is needed for background image to work -->
        <div class="cropit-image-preview-container">
            <div class="cropit-image-preview">
            </div>
        </div>
        <br />
        <input type="range" class="cropit-image-zoom-input" />
    </div>

    <script type="text/javascript">
        $(function() {
            $('#image-cropper').cropit({
                exportZoom: 1.25,
                initialZoom: 0,
                minZoom:0,
                maxZoom:3,
                imageBackground: true,
                imageBackgroundBorderWidth: 20,
                imageState: {
                    src: '<%=CapturedImageFileUrl%>',
                },
            });
        });
                
    </script>

    <script type="text/javascript">
        function CropAndUploadPic() {
            // generate the image data
            var dataURL = $('#image-cropper').cropit('export');
            // Sending the image data to Server
            $.ajax({
                type: 'POST',
                url: "CaptureImageForm.aspx/GetCapturedImage",
                data: { imgBase64: dataURL },
                success: function() {
                    // Send image to parent window
                    GetRadWindow().close(dataURL);
                    return false;
                }
            });
        }
    </script>

</asp:Content>
