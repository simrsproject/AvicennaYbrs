<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaptureImageForm.aspx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.CaptureImageForm" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Capture Picture</title>
    <script src="Plugin/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="Plugin/jquery.webcam.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="radScriptManager" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/JavaScript/Common/Core.js" />
            <asp:ScriptReference Path="~/JavaScript/FormDecorator/RadFormDecorator.js" />
            <asp:ScriptReference Path="~/JavaScript/Common/Popup/PopupScripts.js" />
        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadSkinManager ID="radSkinManager" runat="server" />
    <%--RadFormDecorator DecoratedControls jangan dirubah ke All krn capture webcamnya bisa jadi tidak jalan --%>
    <telerik:RadFormDecorator ID="radFormDecorator" runat="server" DecoratedControls="Buttons"
        EnableEmbeddedScripts="false" />
    <table width="640px">
        <tr>
            <td style="width: 50%">
                <fieldset id="FieldSet1" style="width: 320px; min-height: 240px;">
                    <legend>Live Camera</legend>
                    <div id="webcam">
                    </div>
                    <asp:Button ID="btnCapture" Text="Capture" runat="server" Width="316px" OnClientClick="javascript:CaptureWebcam();void(0);return false;" />
                </fieldset>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <fieldset id="FieldSet2" style="width: 320px; min-height: 276px;">
                    <legend>Captured Picture</legend>
                    <canvas id="canvas" width="320px" height="240px"></canvas>
                    <div id="table">
                        <div class="row">
                            <span class="cell">
                                <asp:Button ID="btnOk" Text="Ok" runat="server" Width="152px" OnClientClick="javascript:UploadPic();return false;" />
                            </span><span class="cell">
                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" Width="152px" OnClientClick="javascript:GetRadWindow().close();return false;" />
                            </span>
                        </div>
                    </div>
                </fieldset>
            </td>
        </tr>
    </table>

    <script type="text/javascript">

        var pos = 0;
        var ctx = null;
        var cam = null;
        var image = null;

        var filter_on = false;
        var filter_id = 0;

        var imgWidth = 320;
        var imgHeight = 240;

        var isHasCaptured = false;

        function changeFilter() {
            if (filter_on) {
                filter_id = (filter_id + 1) & 7;
            }
        }

        function toggleFilter(obj) {
            if (filter_on = !filter_on) {
                obj.parentNode.style.borderColor = "#c00";
            } else {
                obj.parentNode.style.borderColor = "#333";
            }
        }

        jQuery("#webcam").webcam({
            width: imgWidth,
            height: imgHeight,
            mode: "callback",
            swffile: "Plugin/jscam_canvas_only.swf",

            //            onTick: function(remain) {

            //                if (0 == remain) {
            //                    jQuery("#status").text("Cheese!");
            //                } else {
            //                    jQuery("#status").text(remain + " seconds remaining...");
            //                }
            //            },

            onSave: function(data) {

                var col = data.split(";");
                var img = image;

                if (false == filter_on) {

                    for (var i = 0; i < imgWidth; i++) {
                        var tmp = parseInt(col[i]);
                        img.data[pos + 0] = (tmp >> 16) & 0xff;
                        img.data[pos + 1] = (tmp >> 8) & 0xff;
                        img.data[pos + 2] = tmp & 0xff;
                        img.data[pos + 3] = 0xff;
                        pos += 4;
                    }

                } else {

                    var id = filter_id;
                    var r, g, b;
                    var r1 = Math.floor(Math.random() * 255);
                    var r2 = Math.floor(Math.random() * 255);
                    var r3 = Math.floor(Math.random() * 255);

                    for (var i = 0; i < imgWidth; i++) {
                        var tmp = parseInt(col[i]);

                        /* Copied some xcolor methods here to be faster than calling all methods inside of xcolor and to not serve complete library with every req */

                        if (id == 0) {
                            r = (tmp >> 16) & 0xff;
                            g = 0xff;
                            b = 0xff;
                        } else if (id == 1) {
                            r = 0xff;
                            g = (tmp >> 8) & 0xff;
                            b = 0xff;
                        } else if (id == 2) {
                            r = 0xff;
                            g = 0xff;
                            b = tmp & 0xff;
                        } else if (id == 3) {
                            r = 0xff ^ ((tmp >> 16) & 0xff);
                            g = 0xff ^ ((tmp >> 8) & 0xff);
                            b = 0xff ^ (tmp & 0xff);
                        } else if (id == 4) {

                            r = (tmp >> 16) & 0xff;
                            g = (tmp >> 8) & 0xff;
                            b = tmp & 0xff;
                            var v = Math.min(Math.floor(.35 + 13 * (r + g + b) / 60), 255);
                            r = v;
                            g = v;
                            b = v;
                        } else if (id == 5) {
                            r = (tmp >> 16) & 0xff;
                            g = (tmp >> 8) & 0xff;
                            b = tmp & 0xff;
                            if ((r += 32) < 0) r = 0;
                            if ((g += 32) < 0) g = 0;
                            if ((b += 32) < 0) b = 0;
                        } else if (id == 6) {
                            r = (tmp >> 16) & 0xff;
                            g = (tmp >> 8) & 0xff;
                            b = tmp & 0xff;
                            if ((r -= 32) < 0) r = 0;
                            if ((g -= 32) < 0) g = 0;
                            if ((b -= 32) < 0) b = 0;
                        } else if (id == 7) {
                            r = (tmp >> 16) & 0xff;
                            g = (tmp >> 8) & 0xff;
                            b = tmp & 0xff;
                            r = Math.floor(r / 255 * r1);
                            g = Math.floor(g / 255 * r2);
                            b = Math.floor(b / 255 * r3);
                        }

                        img.data[pos + 0] = r;
                        img.data[pos + 1] = g;
                        img.data[pos + 2] = b;
                        img.data[pos + 3] = 0xff;
                        pos += 4;
                    }
                }

                if (pos >= 0x4B000) {
                    ctx.putImageData(img, 0, 0);
                    pos = 0;
                    isHasCaptured = true;
                }
            },

            onCapture: function() {
                webcam.save();
            },

            debug: function(type, string) {

                jQuery("#status").html(type + ": " + string);

            },

            onLoad: function() {

                var cams = webcam.getCameraList();
                for (var i in cams) {
                    jQuery("#cams").append("<li>" + cams[i] + "</li>");
                }
            }

        }

);

        function getPageSize() {

            var xScroll, yScroll;

            if (window.innerHeight && window.scrollMaxY) {
                xScroll = window.innerWidth + window.scrollMaxX;
                yScroll = window.innerHeight + window.scrollMaxY;
            } else if (document.body.scrollHeight > document.body.offsetHeight) { // all but Explorer Mac
                xScroll = document.body.scrollWidth;
                yScroll = document.body.scrollHeight;
            } else { // Explorer Mac...would also work in Explorer 6 Strict, Mozilla and Safari
                xScroll = document.body.offsetWidth;
                yScroll = document.body.offsetHeight;
            }

            var windowWidth, windowHeight;

            if (self.innerHeight) { // all except Explorer
                if (document.documentElement.clientWidth) {
                    windowWidth = document.documentElement.clientWidth;
                } else {
                    windowWidth = self.innerWidth;
                }
                windowHeight = self.innerHeight;
            } else if (document.documentElement && document.documentElement.clientHeight) { // Explorer 6 Strict Mode
                windowWidth = document.documentElement.clientWidth;
                windowHeight = document.documentElement.clientHeight;
            } else if (document.body) { // other Explorers
                windowWidth = document.body.clientWidth;
                windowHeight = document.body.clientHeight;
            }

            // for small pages with total height less then height of the viewport
            if (yScroll < windowHeight) {
                pageHeight = windowHeight;
            } else {
                pageHeight = yScroll;
            }

            // for small pages with total width less then width of the viewport
            if (xScroll < windowWidth) {
                pageWidth = xScroll;
            } else {
                pageWidth = windowWidth;
            }
            return [pageWidth, pageHeight];
        }

        window.addEventListener("load", function() {

            var canvas = document.getElementById("canvas");

            if (canvas.getContext) {
                ctx = document.getElementById("canvas").getContext("2d");
                ctx.clearRect(0, 0, imgWidth, imgHeight);

                var img = new Image();

                img.src = "/static/logo.gif";

                img.onload = function() {
                    ctx.drawImage(img, 129, 89);
                }
                image = ctx.getImageData(0, 0, imgWidth, imgHeight);
            }

        }, false);

        function CaptureWebcam() {
            if (webcam.capture)
                webcam.capture();
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        function UploadPic() {
            if (isHasCaptured === false) {
                alert('Please capture foto first');
                return;
            }
            // generate the image data
            var canvas = document.getElementById("canvas");
            var dataURL = canvas.toDataURL("image/png");

            // Sending the image data to Server
            $.ajax({
                type: 'POST',
                url: "CaptureImageForm.aspx/GetCapturedImage",
                data: { imgBase64: dataURL },
                success: function() {
                    // Send image to parent window
                    //                    GetRadWindow().close('ok');

                    window.location.assign("CropImage.aspx");
                    var oWnd = GetRadWindow();
                    oWnd.setSize(400, 400);
                    oWnd.center();
                    return false;
                }
            });
        }


    </script>

    </form>
</body>
</html>
