<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="RasproFormEnd.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.RasproFormEnd" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Telerik.Web.UI.Skins" %>
<%@ Import Namespace="Temiang.Avicenna.Module.RADT.Emr" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasproFlowChart.ascx" TagPrefix="uc1" TagName="RasproFlowChart" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasproHeader.ascx" TagPrefix="uc1" TagName="RasproHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
        <style>
            .AutoHeightGridClass .rgDataDiv {
                height: auto !important;
            }
        </style>
        <script type="text/javascript">
            function onOkClick() {
                // Tampilkan tandatangan
                var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html';
                var oWnd = $find("<%= winImage.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
            }
            function winImage_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    document.getElementById('<%=hdnImage.ClientID %>').value = arg.image;

                    // Save Raspro
                    __doPostBack("<%= btnOk.UniqueID %>", 'save');
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <asp:HiddenField runat="server" ID="hdnRasproLineID" />
    <asp:HiddenField runat="server" ID="hdnRasproLineSeqNo" />
    <asp:HiddenField runat="server" ID="hdnAbRestrictionID" />
    <asp:HiddenField runat="server" ID="hdnAbLevel" />
    <asp:HiddenField runat="server" ID="hdnImage" />
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Move, Close,Maximize,Resize"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winImage_ClientClose"
        ID="winImage" />

    <uc1:RasproHeader runat="server" ID="rasproHeader" />
    <div style="height: 4px;"></div>
    <div style="width: 40%; margin: auto; width: 600px; border: 3px solid #73AD21; padding: 10px;">
        <%=RasproForm.PreviouseSpecificationHtml((RegistrationRaspro)Session["rr"],(RegistrationRasproLineCollection)Session["rrlcoll"], PrevRasproLineSeqNo)%>
    </div>

    <fieldset style="background-color: lightyellow;">
        <legend>ACTION</legend>
        <asp:Label runat="server" ID="lblAction" Font-Size="14px"></asp:Label>

        <div style="height: 4px;"></div>
        <asp:Panel runat="server" ID="pnlAntibioticSugest">
            <asp:Literal runat="server" ID="litAntibioticSugest"></asp:Literal>
        </asp:Panel>
        <table runat="server" id="tblSelectAction">
            <tr>
                <td class="label">Select Action
                </td>
                <td class="entry">
                    <telerik:RadComboBox ID="cboAction" runat="server" Width="300px" />
                </td>
                <td width="20px"></td>
                <td></td>
            </tr>
        </table>
    </fieldset>

    <asp:Panel runat="server" ID="panMenu">
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Button ID="btnPrev" runat="server" Text="<< Prev" Width="70" OnClick="btnPrev_OnClick" />&nbsp;
                                <asp:Button ID="btnOk" runat="server" Text="Ok" Width="70" OnClientClick="onOkClick();return false;" />&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70" OnClientClick="Close();return false;" />
                </td>
            </tr>
        </table>
    </asp:Panel>


</asp:Content>
