<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="WebCamJCrop.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.KEPK.WebCamJCrop" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- First, include the Webcam.js JavaScript Library -->
    <script type="text/javascript" src="<%=Temiang.Avicenna.Common.Helper.UrlRoot()%>/JavaScript/webcam.min.js"></script>
    <script src="<%=Temiang.Avicenna.Common.Helper.UrlRoot()%>/JavaScript/JCrop/jquery.min.js" type="text/javascript"></script>
    <script src="<%=Temiang.Avicenna.Common.Helper.UrlRoot()%>/JavaScript/JCrop/jquery.Jcrop.min.js" type="text/javascript"></script>

    <link rel="stylesheet" href="<%=Temiang.Avicenna.Common.Helper.UrlRoot()%>/JavaScript/JCrop/jquery.Jcrop.min.css" type="text/css" />

    <div id="my_camera" style="margin-left: 10px; margin-top: 10px;"></div>

    <canvas id="cropbox" width="800px" height="600px" style="display: none;"></canvas>
    <canvas id="croped" style="display: none;"></canvas>
    <input type="hidden" id="x" name="x" value="0" />
    <input type="hidden" id="y" name="y" value="0" />
    <input type="hidden" id="w" name="w" value="0" />
    <input type="hidden" id="h" name="h" value="0" />


    <!-- Configure a few settings and attach camera -->
    <script language="JavaScript" type="text/javascript">
        var _wcWidth =  <%= AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamWidth) %>;
        var _wcHeight =  <%= AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamHeight) %>;

        Webcam.set({
            // live preview size
            width: _wcWidth,
            height: _wcHeight,

            //// device capture size
            //dest_width: _wcWidth,
            //dest_height: _wcHeight,

            // format and quality
            image_format: 'png',
            jpeg_quality: 90
        });

        var cropbox = document.getElementById("cropbox");
        cropbox.height = _wcHeight;
        cropbox.width = _wcWidth;

        Webcam.attach('#my_camera');

    </script>

    <!-- A button for taking snaps -->
    <table width="100%">
        <tr>
            <td align="center">
                <div id="pre_take_buttons" class="footer" style="padding-bottom: 8px;">
                    <br />
                    <input type="button" value="Take Snapshot" onclick="preview_snapshot()">
                    <input type="button" value="Cancel" style="width: 70px;" onclick="closeWebCam()">
                </div>
                <div id="prepost_take_buttons" style="display: none; padding-bottom: 8px;" class="footer">
                    <br />
                    <input type="button" value="Take Another" onclick="cancel_preview()">
                    <input type="button" value="Crop" style="width: 70px;" onclick="take_snapshot()">
                    <input type="button" value="Use Image" onclick="closeAndReturnImage()">
                    <input type="button" value="Cancel" style="width: 70px;" onclick="closeWebCam()">
                </div>
            </td>
        </tr>
    </table>

    <!-- Code to handle taking the snapshot and displaying it locally -->
    <script language="JavaScript" type="text/javascript">
        var jcrop_api;
        var _image = new Image();

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

                Webcam.reset(); //Close WebCam

                // Hide Webcam
                document.getElementById('pre_take_buttons').style.display = 'none';
                document.getElementById('prepost_take_buttons').style.display = 'none';
                document.getElementById('my_camera').style.display = 'none';

                // Show Crop Util
                document.getElementById('cropbox').style.display = '';
                document.getElementById('post_take_buttons').style.display = '';

                // Set Image
                _image = new Image();
                //base_image.setAttribute('crossOrigin', 'anonymous');
                _image.onload = function () {
                    document.getElementById('cropbox').getContext('2d').drawImage(_image, 0, 0);

                    //var canvas = document.getElementById('cropbox');
                    //var ctx = canvas.getContext('2d');
                    //ctx.drawImage(image,canvas.width/2-_image.width/2,canvas.height/2-_image.width/2);
                };
                _image.src = data_uri;

                // Jcrop Initialized
                jcrop_api = $.Jcrop('#cropbox', {
                    aspectRatio: 1,
                    onSelect: updateCoords,
                    onChange: updateCoords,
                    onRelease: resetCoords,
                    allowSelect: true,
                    allowMove: true,
                    allowResize: true,
                    aspectRatio: 0
                });

                // Set default select
                jcrop_api.setSelect(getFirstAreaCrop());

                $('#ar_lock').change(function (e) {

                    jcrop_api.setOptions(this.checked ? { aspectRatio: 4 / 3 } : { aspectRatio: 0 });
                    jcrop_api.focus();
                });

            });
        }

        function getFirstAreaCrop() {
            return [0, 0, 800, 600];
        };
        function updateCoords(c) {
            $('#x').val(c.x);
            $('#y').val(c.y);
            $('#w').val(c.w);
            $('#h').val(c.h);
        };

        function resetCoords() {
            $('#x').val(0);
            $('#y').val(0);
            $('#w').val(0);
            $('#h').val(0);
        };
        function closeAndReturnImage() {
            Webcam.snap(function (data_uri) {
                Webcam.reset(); //Close WebCam
                // Send image to parent window
                GetRadWindow().close(data_uri);
            });
        }
        function closeAndReturnCroppedImage() {
            // generate the image data
            var dataURL = imgCropped();

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

        function imgCropped() {
            var x = parseInt($('#x').val())
            var y = parseInt($('#y').val())
            var w = parseInt($('#w').val())
            var h = parseInt($('#h').val())

            if (w > 0 && h > 0) {
                //var source = $("#cropbox")[0];
                //var croped = $("#croped")[0];
                //var context = croped.getContext("2d");
                //context.drawImage(source, x, y, w, h, 0, 0, croped.width, croped.height);

                //return croped.toDataURL("image/jpeg");

                var sourceCtx = document.getElementById("cropbox").getContext("2d");
                var imgData = sourceCtx.getImageData(x, y, w, h);

                var croped = document.getElementById("croped");
                // Set canvas size
                croped.width = w;
                croped.height = h;

                // Draw with croped image
                var cropedCtx = croped.getContext("2d");
                cropedCtx.putImageData(imgData, 0, 0);

                // Return value
                //return croped.toDataURL("image/jpg"); // Format jpg gagal waktu disave ke file
                return croped.toDataURL("image/png");
            }
            else {
                //No crop
                var data = document.getElementById("cropbox").getContext("2d").canvas.toDataURL("image/png");
                return data;
            }
        }
    </script>

    <script language="JavaScript" type="text/javascript">
        var angleInDegrees = 0;

        function drawRotated(degrees) {
            var canvas = document.getElementById("cropbox");
            var ctx = canvas.getContext("2d");
            ctx.clearRect(0, 0, _wcWidth, _wcHeight);
            ctx.save();
            ctx.translate(canvas.width / 2, canvas.height / 2);
            ctx.rotate(degrees * Math.PI / 180);
            ctx.drawImage(_image, - _wcWidth/2, -_wcHeight/2);
            ctx.restore();
        }

        function rotateClockWise() {
            angleInDegrees += 90;
            drawRotated(angleInDegrees);
        }

        function rotateCounterClockWise() {
            angleInDegrees -= 90;
            drawRotated(angleInDegrees);
        }

    </script>
    <label style="display: none;">
        <input type="checkbox" id="ar_lock" />Aspect ratio</label>

    <div id="post_take_buttons" style="display: none; padding-bottom: 4px;" class="footer">
        <table width="100%">
            <tr>
                <td align="center">
                    <input type="button" onclick="rotateClockWise()" value="Rotate right" />
                    <input type="button" onclick="rotateCounterClockWise()" value="Rotate left" />&nbsp;&nbsp;
                    <input type="button" value="Ok" style="width: 70px;" onclick="closeAndReturnCroppedImage()" />&nbsp;
                    <input type="button" value="Cancel" style="width: 70px;" onclick="closeWebCam()" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>