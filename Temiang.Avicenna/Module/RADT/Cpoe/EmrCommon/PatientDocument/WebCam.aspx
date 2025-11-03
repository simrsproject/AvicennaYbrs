<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="WebCam.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.WebCam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        /* Limit the background image by adding overflow: hidden */
        #image-cropper {
            overflow: hidden;
        }

        .cropit-image-preview {
            background-color: #f8f8f8;
            background-size: cover;
            border: 5px solid #ccc;
            border-radius: 3px;
            margin-top: 7px;
            width: 640px;
            height: 480px;
            cursor: move;
        }
        /* Translucent background image */
        .cropit-image-background {
            opacity: .2;
            cursor: auto;
        }

        .image-size-label {
            margin-top: 10px;
        }
        /*
         * If the slider or anything else is covered by the background image,
         * use relative or absolute position on it
         */
        input.cropit-image-zoom-input {
            position: relative;
            width: 640px;
        }
    </style>

    <div id="my_camera" style="margin-left:10px;margin-top:10px;"></div>

    <!-- First, include the Webcam.js JavaScript Library -->
    <script type="text/javascript" src="<%=Temiang.Avicenna.Common.Helper.UrlRoot()%>/JavaScript/webcam.min.js"></script>
    <script src="<%=Temiang.Avicenna.Common.Helper.UrlRoot()%>/JavaScript/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="<%=Temiang.Avicenna.Common.Helper.UrlRoot()%>/JavaScript/jquery.cropit.js" type="text/javascript"></script>
    <!-- Configure a few settings and attach camera -->
    <script language="JavaScript">
        Webcam.set({
            // live preview size
            width: 640,
            height: 480,

            // device capture size
            dest_width: 640,
            dest_height: 480,

            // format and quality
            image_format: 'jpeg',
            jpeg_quality: 50
        });

        Webcam.attach('#my_camera');
    </script>

    <!-- A button for taking snaps -->
    <table width="100%">
        <tr>
            <td align="center">
                <div id="pre_take_buttons">
                    <br />
                    <input type="button" value="Take Snapshot" onclick="preview_snapshot()">
                    <input type="button" value="Cancel" onclick="closeWebCam()">
                </div>
                <div id="prepost_take_buttons" style="display: none">
                    <br />
                    <input type="button" value="Take Another" onclick="cancel_preview()">
                    <input type="button" value="Use Snapshot" onclick="take_snapshot()">
                </div>
            </td>
        </tr>
    </table>

    <!-- Code to handle taking the snapshot and displaying it locally -->
    <script language="JavaScript">
        // preload shutter audio clip
        var _shutter = new Audio();
        _shutter.autoplay = false;
        _shutter.src = navigator.userAgent.match(/Firefox/) ? '<%=Temiang.Avicenna.Common.Helper.UrlRoot()%>/Audio/shutter.ogg' : '<%=Temiang.Avicenna.Common.Helper.UrlRoot()%>/Audio/shutter.mp3';

        document.title = "Take Snapshot";

        function preview_snapshot() {

            // play sound effect
            _shutter.play();

            // freeze camera so user can preview pic
            Webcam.freeze();

            // swap button sets
            document.getElementById('pre_take_buttons').style.display = 'none';
            document.getElementById('prepost_take_buttons').style.display = '';
        }

        function cancel_preview() {
            document.title = "Take Snapshot";
            // cancel preview freeze and return to live camera feed
            Webcam.unfreeze();

            // swap buttons back
            document.getElementById('pre_take_buttons').style.display = '';
            document.getElementById('prepost_take_buttons').style.display = 'none';
        }

        function take_snapshot() {
            document.title = "Crop Image";
            // take snapshot and get image data
            Webcam.snap(function (data_uri) {
                $('#image-cropper').cropit({
                    exportZoom: 1.25,
                    initialZoom: 0,
                    minZoom: 0,
                    maxZoom: 3,
                    imageBackground: true,
                    imageBackgroundBorderWidth: 20,
                    imageState: {
                        src: data_uri
                    },
                });

                Webcam.reset(); //Close WebCam

                // Hide Webcam
                document.getElementById('pre_take_buttons').style.display = 'none';
                document.getElementById('prepost_take_buttons').style.display = 'none';
                document.getElementById('my_camera').style.display = 'none';

                // Show Crop Util
                document.getElementById('image-cropper').style.display = '';
                document.getElementById('post_take_buttons').style.display = '';
            });
        }

        function closeAndReturnImage() {
            // generate the image data
            var dataURL = $('#image-cropper').cropit('export');

            // Send image to parent window
            GetRadWindow().close(dataURL);
        }
        function closeWebCam() {
            Webcam.reset(); //Close WebCam

            // Pakai fungsi Close() tidak jalan
            var oWnd = GetRadWindow();
            oWnd.argument = new Object(); // Selalu reset krn value sebelumnya masih ada (hasil CloseAndApply)
            oWnd.close();
        }
    </script>

    <div id="image-cropper" style="margin-left:5px; display: none">
        <!-- .cropit-image-preview-container is needed for background image to work -->
        <div class="cropit-image-preview-container">
            <div class="cropit-image-preview">
            </div>
        </div>
        <br />
        <input type="range" class="cropit-image-zoom-input" />
    </div>
    <div id="post_take_buttons" style="display: none">
        <table width="100%">
            <tr>
                <td align="center">
                    <input type="button" value="Ok" onclick="closeAndReturnImage()">&nbsp;
        <input type="button" value="Cancel" onclick="closeWebCam()">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
