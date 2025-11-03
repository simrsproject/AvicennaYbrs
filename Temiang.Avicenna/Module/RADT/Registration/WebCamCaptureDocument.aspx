<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebCamCaptureDocument.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.WebCamCaptureDocument" %>

<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <telerik:RadScriptManager ID="radScriptManager" runat="server">
            <Scripts>
                <%-- Common --%>
                <asp:ScriptReference Path="~/JavaScript/Common/Core.js" />
                <asp:ScriptReference Path="~/JavaScript/Common/jQuery.js" />
                <asp:ScriptReference Path="~/JavaScript/Common/jQueryPlugins.js" />
                <asp:ScriptReference Path="~/JavaScript/Common/Navigation/NavigationScripts.js" />
                <asp:ScriptReference Path="~/JavaScript/Common/Navigation/OverlayScript.js" />
                <asp:ScriptReference Path="~/JavaScript/Common/AnimationFramework/AnimationFramework.js" />

                <%-- RadFormDecorator --%>
                <asp:ScriptReference Path="~/JavaScript/FormDecorator/RadFormDecorator.js" />
                <asp:ScriptReference Path="~/JavaScript/Common/Popup/PopupScripts.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadSkinManager ID="radSkinManager" runat="server" />
        <telerik:RadFormDecorator ID="radFormDecorator" runat="server" EnableEmbeddedScripts="false" DecoratedControls="Default" />
        <div style="resize: none; overflow: hidden; width: 810px; height: 620px;">
            <div id="my_camera" style="margin-left: 10px; margin-top: 10px; overflow: visible; width: 800px; height: 600px;"></div>
            <canvas id="cropbox" width="800" height="600" style="overflow: auto; display: none;"></canvas>
        </div>
        <canvas id="croped" style="display: none;"></canvas>
        <input type="hidden" id="x" name="x" value="0" />
        <input type="hidden" id="y" name="y" value="0" />
        <input type="hidden" id="w" name="w" value="0" />
        <input type="hidden" id="h" name="h" value="0" />

        <!-- A button for taking snaps -->
        <table width="100%">
            <tr>
                <td align="center">
                    <div id="pre_take_buttons" class="footer" style="padding-bottom: 8px;">
                        <br />
                        <input type="radio" id="documentMode" name="mode" value="doc" onclick="startWebcamDocumentMode()" checked="checked" />
                        <label for="documentMode">Document Mode</label>&nbsp;
                        <input type="radio" id="idcardMode" name="mode" value="idc" onclick="startWebcamIdcardMode()" />
                        <label for="idcardMode">ID Card Mode</label>&nbsp;&nbsp;&nbsp;

                        <input type="button" value="Take Snapshot" onclick="preview_snapshot()" />
                        <input type="button" value="Cancel" style="width: 70px;" onclick="closeWebCam()" />
                    </div>
                    <div id="prepost_take_buttons" style="display: none; padding-bottom: 8px;" class="footer">
                        <br />
                        <input type="button" value="Take Another" onclick="cancel_preview()" />
                        <input type="button" value="Crop" style="width: 70px;" onclick="take_snapshot()" />
                        <input type="button" value="Use Image" onclick="closeAndSaveFullImage()" />
                        <input type="button" value="Cancel" style="width: 70px;" onclick="closeWebCam()" />
                    </div>
                </td>
            </tr>
        </table>



        <label style="display: none;">
            <input type="checkbox" id="ar_lock" />Aspect ratio</label>

        <div id="post_take_buttons" style="display: none; padding-bottom: 4px;" class="footer">
            <table width="100%">
                <tr>
                    <td align="center">
                        <input type="button" value="Ok" style="width: 70px;" onclick="closeAndSaveCroppedImage()" />&nbsp;
        <input type="button" value="Cancel" style="width: 70px;" onclick="closeWebCam()" />
                    </td>
                </tr>
            </table>
        </div>

        <telerik:RadCodeBlock runat="server" ID="cdBlock">
            <%--First, include the Webcam.js JavaScript Library--%>
            <script type="text/javascript" src="<%=Temiang.Avicenna.Common.Helper.UrlRoot()%>/JavaScript/webcam.min.js"></script>
            <script src="<%=Temiang.Avicenna.Common.Helper.UrlRoot()%>/JavaScript/JCrop/jquery.min.js" type="text/javascript"></script>
            <script src="<%=Temiang.Avicenna.Common.Helper.UrlRoot()%>/JavaScript/JCrop/jquery.Jcrop.min.js" type="text/javascript"></script>
            <link rel="stylesheet" href="<%=Temiang.Avicenna.Common.Helper.UrlRoot()%>/JavaScript/JCrop/jquery.Jcrop.min.css" type="text/css" />

            <%--Configure a few settings and attach camera--%>
            <script type="text/javascript">
                var _wcWidth =  <%= AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamWidth) %>;
                var _wcHeight =  <%= AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamHeight) %>;

                function startWebcamDocumentMode() {
                    Webcam.reset(); //Close WebCam
                    Webcam.set({
                        // live preview size
                        width: _wcWidth,
                        height: _wcHeight,

                        // device capture size
                        dest_width: _wcWidth,
                        dest_height: _wcHeight,

                        // format and quality
                        image_format: 'png',
                        jpeg_quality: 90
                    });
                    var cropbox = document.getElementById("cropbox");
                    cropbox.height = _wcHeight;
                    cropbox.width = _wcWidth;

                    Webcam.attach('#my_camera');
                }

                function startWebcamIdcardMode() {
                    var wcmWidth =  <%= AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamMaxWidth) %>;
                    var wcmHeight =  <%= AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamMaxHeight) %>;

                    Webcam.reset(); //Close WebCam
                    Webcam.set({
                        // live preview size
                        width: wcmWidth,
                        height: wcmHeight,

                        // device capture size
                        dest_width: wcmWidth,
                        dest_height: wcmHeight,

                        // format and quality
                        image_format: 'png',
                        jpeg_quality: 90,

                        constraints: {
                            width: { exact: wcmWidth },
                            height: { exact: wcmHeight }
                        }
                    });

                    var cropbox = document.getElementById("cropbox");
                    cropbox.height = wcmWidth;
                    cropbox.width = wcmHeight;

                    Webcam.attach('#my_camera');
                }

                startWebcamDocumentMode();


                //Code to handle taking the snapshot and displaying it locally
                var jcrop_api;
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
                        var base_image = new Image();
                        //base_image.setAttribute('crossOrigin', 'anonymous');
                        base_image.onload = function () {
                            document.getElementById('cropbox').getContext('2d').drawImage(base_image, 0, 0);
                        };
                        base_image.src = data_uri;

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
                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow) oWindow = window.radWindow;
                    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                    return oWindow;
                }
                function closeAndSaveImage(val) {
                    var submitval = JSON.stringify({ data: val });
                    // Sending the image data to Server
                    $.ajax({
                        type: 'POST',
                        url: "WebCamCaptureDocument.aspx/SaveToFile",
                        contentType: "application/json;charset=utf-8",
                        datatype: "json",
                        data: submitval,
                        success: function (response) {
                            alert("Document Saved");
                            // Retval
                            var oArg = new Object();
                            oArg.callbackMethod = '<%=Request.QueryString["ccm"]%>';
                            oArg.eventArgument = '<%=Request.QueryString["cea"]%>';
                            oArg.eventTarget = '<%=Request.QueryString["cet"]%>';

                            // Retval from Dummy WebMethod 
                            oArg.count = decodeURI(response.d);

                            //get a reference to the current RadWindow
                            var oWnd = GetRadWindow();

                            //Close the RadWindow            
                            oWnd.close(oArg);
                        },
                        error: function (xhr, status, error) {
                            var errorMessage = xhr.status + ': ' + xhr.statusText
                            alert('Error - ' + errorMessage);
                        }
                    });
                }
                function closeAndSaveFullImage() {
                    document.getElementById('my_camera').style.display = 'none';
                    document.getElementById('cropbox').style.display = '';

                    Webcam.snap(function (data_uri) {
                        // freeze camera so user can preview pic
                        Webcam.freeze();

                        // Set Image
                        var base_image = new Image();
                        //base_image.setAttribute('crossOrigin', 'anonymous');
                        base_image.onload = function () {
                            document.getElementById('cropbox').getContext('2d').drawImage(base_image, 0, 0);

                            // Set full area for crop
                            $('#x').val(0);
                            $('#y').val(0);
                            $('#w').val(_wcWidth);
                            $('#h').val(_wcHeight);

                            closeAndSaveCroppedImage();

                            Webcam.reset(); //Close WebCam
                        };
                        base_image.src = data_uri;
                    });
                }
                function closeAndSaveCroppedImage() {
                    var data = imgCropped() +"|<%=RegistrationNo%>|<%=Request.QueryString["dfid"]%>";
                    closeAndSaveImage(data);
                }

            </script>
        </telerik:RadCodeBlock>
    </form>
</body>
</html>
