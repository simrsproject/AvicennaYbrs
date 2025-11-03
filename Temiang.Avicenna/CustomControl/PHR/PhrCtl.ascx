<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhrCtl.ascx.cs" Inherits="Temiang.Avicenna.CustomControl.Phr.PhrCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<telerik:RadCodeBlock ID="radCodeBlock" runat="server">
    <script type="text/javascript">
        function ResetValue(ctlID, method, caption) {
            if (!confirm("Reset Value " + caption + "?")) return;
            var obj = {};
            obj.patientID = "<%= PatientID %>";
            obj.registrationNo = "<%= RegistrationNo %>";
            obj.fromRegistrationNo = "<%= FromRegistrationNo %>";
            $.ajax({
                url: '<%= Helper.UrlRoot() %>/CustomControl/PHR/PhrWebService.asmx/' + method,
                data: JSON.stringify(obj), //ur data to be sent to server
                contentType: "application/json; charset=utf-8",
                type: "POST",
                dataType: "json",
                success: function (data) {
                    var str = data.d;
                    if ((str === null) || (str === ''))
                        alert("Data not found");
                    else {
                        ctlID = "<%= ClientID %>_" + ctlID;
                        //$find(ctlID).set_value(data.d);
                        var ctl = document.getElementById(ctlID);

                        str = str.replaceAll('<li>','•');
                        str = str.replaceAll('</li>','\n');
                        str = str.replaceAll('<ul>','');
                        str = str.replaceAll('</ul>','');
                        str = str.replaceAll('<br />','\n');
                        ctl.value = str.replace( /(<([^>]+)>)/ig, '');;
                        autosize.update(ctl);
                    }
                },
                error: function (x, y, z) {
                    alert(x.responseText + "  " + x.status);
                }
            });
        }

        function winImage_ClientClose(oWnd, args) {
            oWnd.setUrl("about:blank"); // Sets url to blank for release variable
            var arg = args.get_argument();
            if (arg != null) {
                document.getElementById(arg.txtId).value = arg.image;
                var img = document.getElementById(arg.imgId);

                if (arg.image.includes("base64"))
                    img.setAttribute('src', arg.image);
                else
                    img.setAttribute('src', "data:image/Png;base64," + arg.image);
            }
        }


        function editImage(mod, imgId, txtId) {
            var dataImg = document.getElementById(txtId).value;
            // Sending the image data to Server
            $.ajax({
                type: 'POST',
                url: "<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/ImageEditSession.aspx/Dummy",
                    data: { imgBase64: dataImg },
                    success: function () {
                        var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/ImageEdit.aspx?mod=' + mod + '&imgId=' + imgId + '&txtId=' + txtId;
                        var oWnd = $find("<%= winImage.ClientID %>");
                        oWnd.setUrl(url);
                        oWnd.show();
                        return false;
                    }
                });
        }

        function editSignature(mod, imgId, txtId) {
            var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html?mod=' + mod + '&imgId=' + imgId + '&txtId=' + txtId;
            var oWnd = $find("<%= winImage.ClientID %>");
            oWnd.setUrl(url);
            oWnd.show();
        }

        var _wcWidth =  <%= AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamWidth) %>;
        var _wcHeight =  <%= AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamHeight) %>;

        function openWebCam(imgId, txtId) {
            var oWnd = $find("<%= winImage.ClientID %>");
            oWnd.setUrl("<%= Helper.UrlRoot() %>/CustomControl/PHR/InputControl/Webcam/WebCamJCrop.aspx?imgId=" + imgId + "&txtId=" + txtId);
            oWnd.setSize(_wcWidth + 40, _wcHeight + 80);
            oWnd.center();
            oWnd.show();
        }
    </script>
</telerik:RadCodeBlock>
<telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Move, Close,Maximize,Resize"
    ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winImage_ClientClose"
    ID="winImage" />
<asp:HiddenField runat="server" ID="hdnRegistrationNo" />
<asp:HiddenField runat="server" ID="hdnFromRegistrationNo" />
<asp:HiddenField runat="server" ID="hdnPatientID" />
