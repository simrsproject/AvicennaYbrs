<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="RasproFormLine.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.RasproFormLine" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Telerik.Web.UI.Skins" %>
<%@ Import Namespace="Temiang.Avicenna.Module.RADT.Emr" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasproFlowChart.ascx" TagPrefix="uc1" TagName="RasproFlowChart" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasproHeader.ascx" TagPrefix="uc1" TagName="RasproHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hdnRasproLineID" />
    <asp:HiddenField runat="server" ID="hdnRasproLineSeqNo" />
    <uc1:RasproHeader runat="server" ID="rasproHeader" />
    <div style="height: 4px;"></div>
    <div style="margin: auto; width: 1000px; border: 3px solid #73AD21; padding: 10px; font-size:medium;">
        <%=RasproForm.PreviouseSpecificationHtml((RegistrationRaspro)Session["rr"],(RegistrationRasproLineCollection)Session["rrlcoll"], PrevRasproLineSeqNo)%>
        <br />
        <div style="height: 4px;"></div>
        <hr style="clear: both; background-color: #666; height: 1px; border: 0;" />
        <asp:Label runat="server" ID="lblSpecification" Font-Size="Large"></asp:Label><br />
        <div style="padding-left: 20px; padding-top: 10px;">
            <telerik:RadRadioButtonList runat="server" ID="optConddition" AutoPostBack="true" OnSelectedIndexChanged="optConddition_SelectedIndexChanged" Font-Size="Medium">
                <Items>
                    <telerik:ButtonListItem Text="Ya" Value="1" />
                    <telerik:ButtonListItem Text="Tidak" Value="0" />
                </Items>
            </telerik:RadRadioButtonList>
        </div>

        <asp:Panel runat="server" ID="panMenu">
            <table width="100%">
                <tr>
                    <td align="center" >
                        <asp:Button ID="btnPrev" runat="server" Text="<< Prev" Width="70" OnClick="btnPrev_OnClick" />&nbsp;
                    <asp:Button ID="btnNext" runat="server" Text="Next >>" Width="70" OnClick="btnNext_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70" OnClientClick="Close();return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>

</asp:Content>
