<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="GuarantorDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.GuarantorDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/AddressCtl.ascx" TagName="Address" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../../../../JavaScript/DateFormat.js"></script>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function openWinGuarantorInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var oguar = $find("<%= txtGuarantorID.ClientID %>");
                var lblToBeUpdate = "<%= lblGuarantorInfo.ClientID %>";
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Finance/Master/GuarantorInfo/GuarantorInfoDialog.aspx?id=' + oguar.get_value() + '&lblGuarantorInfo=' + lblToBeUpdate + '")%>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function openWinPickList(itemType) {
                var oWnd = $find("<%= winPickList.ClientID %>");

                oWnd.setUrl('ItemPickerList.aspx?itemtype=' + itemType);
                oWnd.set_title('Item List');
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.rebind != null)
                    __doPostBack("<%= grdGuarantorItemRestrictions.UniqueID %>", "rebind");
            }

        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="600px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="onClientClose"
        ID="winPickList">
    </telerik:RadWindow>

    <telerik:RadTabStrip ID="RadTabStrip4" runat="server" MultiPageID="rmpMain">
        <Tabs>
            <telerik:RadTab runat="server" Text="Guarantor Detail" Selected="True" PageViewID="pgMain">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Contract Summary" PageViewID="pgContractSum">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Related Companies" PageViewID="pgRelatedComps">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmpMain" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgMain" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor ID"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadTextBox ID="txtGuarantorID" runat="server" Width="100px" MaxLength="10" />
                                            </td>
                                            <td>&nbsp; <a href="javascript:void(0);" onclick="javascript:openWinGuarantorInfo();"
                                                class="noti_Container">
                                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblGuarantorInfo" AssociatedControlID="txtGuarantorID"
                                                    Text=""></asp:Label>&nbsp; </a>
                                            </td>
                                            <td style="width: 50px"></td>
                                            <td>
                                                <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvGuarantorID" runat="server" ErrorMessage="Guarantor ID required."
                                        ValidationGroup="entry" ControlToValidate="txtGuarantorID" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGuarantorName" runat="server" Text="Guarantor Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtGuarantorName" runat="server" Width="300px" MaxLength="100" TextMode="MultiLine" Height="40px" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvGuarantorName" runat="server" ErrorMessage="Guarantor Name required."
                                        ValidationGroup="entry" ControlToValidate="txtGuarantorName" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGuarantorHeaderID" runat="server" Text="Guarantor Group"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboGuarantorHeaderID" Width="300px" EnableLoadOnDemand="true"
                                        HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboGuarantorHeaderID_ItemDataBound"
                                        OnItemsRequested="cboGuarantorHeaderID_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "GuarantorID")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "GuarantorName")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblShortName" runat="server" Text="Short Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtShortName" runat="server" Width="100px" MaxLength="35" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGuarantorType" runat="server" Text="Guarantor Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRGuarantorType" runat="server" Width="300px" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvGuarantorType" runat="server" ErrorMessage="Guarantor Type required."
                                        ValidationGroup="entry" ControlToValidate="cboSRGuarantorType" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblContractNumber" runat="server" Text="Contract Number"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtContractNumber" runat="server" Width="300px" MaxLength="50" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblContractStart" runat="server" Text="Contract Period"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtContractStart" runat="server" Width="100px" />
                                            </td>
                                            <td>&nbsp;To&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtContractEnd" runat="server" Width="100px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtContactPerson" runat="server" Width="300px" MaxLength="500" Height="100px"
                                        TextMode="MultiLine" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvContactPerson" runat="server" ErrorMessage="Contact Person required."
                                        ValidationGroup="entry" ControlToValidate="txtContactPerson" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBusinessMethod" runat="server" Text="Business Method"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRBusinessMethod" runat="server" Width="300px" AutoPostBack="True"
                                        OnSelectedIndexChanged="cboSRBusinessMethod_SelectedIndexChanged" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvBusinessMethod" runat="server" ErrorMessage="Business Method required."
                                        ValidationGroup="entry" ControlToValidate="cboSRBusinessMethod" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRTariffType" runat="server" Text="Tariff Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRTariffType" runat="server" Width="300px" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvSRTariffType" runat="server" ErrorMessage="Tariff Type required."
                                        ValidationGroup="entry" ControlToValidate="cboSRTariffType" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRGuarantorRuleType" runat="server" Text="Rule Type Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadComboBox ID="cboSRGuarantorRuleType" runat="server" Width="220px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="cboSRGuarantorRuleType_SelectedIndexChanged">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkIsAmountInPercent" runat="server" Text="In Percent" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td class="label" style="width: 33%">
                                                <asp:Label ID="Label2" runat="server" Text="IPR/Default"></asp:Label>
                                            </td>
                                            <td class="label" style="width: 33%">
                                                <asp:Label ID="Label3" runat="server" Text="OPR"></asp:Label>
                                            </td>
                                            <td class="label" style="width: 33%">
                                                <asp:Label ID="Label4" runat="server" Text="EMR"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAmountValue" runat="server" Text="Amount Value"></asp:Label>
                                </td>
                                <td>
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="entry" style="width: 33%">
                                                <telerik:RadNumericTextBox ID="txtAmountValue" runat="server" Width="100px" />
                                            </td>
                                            <td class="entry" style="width: 33%">
                                                <telerik:RadNumericTextBox ID="txtOutpatientAmountValue" runat="server" Width="100px" />
                                            </td>
                                            <td class="entry" style="width: 33%">
                                                <telerik:RadNumericTextBox ID="txtEmergencyAmountValue" runat="server" Width="100px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr height="24px">
                                <td class="label"></td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIPRAmountDefault" runat="server" Text="Item Rule using Default Amount Value (IPR)" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr style="display: none">
                                <td class="label">
                                    <asp:Label ID="lblPlavonType" runat="server" Text="Plafond Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIsPlavonTypeGlobal" runat="server" Text="Global Plafond" Enabled="False" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label"></td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkExcessPlafondToDiscount" runat="server" Text="Excess Plafond To Discount" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:CheckBox ID="chkIsDiscountProrataToRevenue" runat="server" Text="Is Discount Prorata To Revenue" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRoundingTransaction" runat="server" Text="Bill Rounding"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="entry" style="width: 33%">
                                                <telerik:RadNumericTextBox ID="txtRoundingTransaction" runat="server" Width="100px" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkIsUsingRoundingDown" runat="server" Text="Rounding Down" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblReportRLID" runat="server" Text="Report RL ID"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboReportRLID" runat="server" Width="300px" AllowCustomText="true"
                                        Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboReportRLID_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRlMasterReportItemID" runat="server" Text="Report RL Detail ID"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboRlMasterReportItemID" runat="server" Width="300px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr height="24px">
                                <td class="label">Tariff Calculation Method
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rblTariffCalculation" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="true" Value="0">By Transaction Date</asp:ListItem>
                                        <asp:ListItem Value="1">By Registration Date</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr height="24px">
                                <td class="label">
                                    <asp:Label ID="lblRegistrationTypeCovered" runat="server" Text="Cover Registration Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIsCoverInpatient" runat="server" Text="Inpatient" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkIsCoverOutpatient" runat="server" Text="Outpatient" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblIsIncludeItem" runat="server" Text="Cover Item Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIsIncludeItemMedical" runat="server" Text="Medical" />
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rblToGuarantorMedical" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="true">To Guarantor</asp:ListItem>
                                                    <asp:ListItem>To Patient</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIsIncludeItemNonMedical" runat="server" Text="Non Medical" />
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rblToGuarantorNonMedical" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="true">To Guarantor</asp:ListItem>
                                                    <asp:ListItem>To Patient</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td>
                                                <asp:CheckBox ID="chkIsIncludeItemOptic" runat="server" Text="Optic" />
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rblToGuarantorOptic" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="true">To Guarantor</asp:ListItem>
                                                    <asp:ListItem>To Patient</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblItemMedicMargin" runat="server" Text="Item Medic Margin"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtItemMedicMarginPercentage" runat="server" Type="Percent"
                                                    Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" />
                                            </td>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cboItemMedicMarginID" runat="server" Width="190px">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvItemMedicMarginPercentage" runat="server" ErrorMessage="Item Medic Margin Percentage required."
                                        ValidationGroup="entry" ControlToValidate="txtItemMedicMarginPercentage" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblItemNonMedicMargin" runat="server" Text="Item Non Medic Margin"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtItemNonMedicMarginPercentage" runat="server" Type="Percent"
                                                    Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" />
                                            </td>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cboItemNonMedicMarginID" runat="server" Width="190px">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvItemNonMedicMarginPercentage" runat="server" ErrorMessage="Item Non Medic Margin Percentage required."
                                        ValidationGroup="entry" ControlToValidate="txtItemNonMedicMarginPercentage" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label"></td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIsIncludeAdminValue" runat="server" Text="Cover Administration"
                                                    AutoPostBack="True" OnCheckedChanged="chkIsIncludeAdminValue_CheckedChanged" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:CheckBox ID="chkIsCoverAllAdminCosts" runat="server" Text="Cover All Administration Cost" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr height="24px">
                                <td class="label"></td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIsAdminFromTotal" runat="server" Text="Admin Calculation From Total Transaction" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr height="24px">
                                <td class="label"></td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIsAdminCalcBeforeDiscount" runat="server" Text="Admin Calculation Before Discount" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblAdminInpatient" runat="server" Text="Inpatient"></asp:Label>
                                            </td>
                                            <td class="label">
                                                <asp:Label ID="lblAdminOutpatient" runat="server" Text="Outpatient"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAdminPercentage" runat="server" Text="Admin Percentage"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtAdminPercentage" runat="server" Type="Percent"
                                                    MinValue="0" />
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtAdminPercentageOp" runat="server" Type="Percent"
                                                    MinValue="0" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAdminAmountLimit" runat="server" Text="Admin Amount Limit (Min)"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtAdminAmountMin" runat="server" MinValue="0" />
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtAdminAmountMinOp" runat="server" MinValue="0" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAdminAmountMax" runat="server" Text="Admin Amount Limit (Max)"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtAdminAmountMax" runat="server" MinValue="0" />
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtAdminAmountMaxOp" runat="server" MinValue="0" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRPaymentType" runat="server" Text="A/R Payment Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRPaymentType" runat="server" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr runat="server" id="trSRPhysicianFeeCategory">
                                <td class="label">
                                    <asp:Label ID="lblSRPhysicianFeeType" runat="server" Text="Physician Fee Category"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRPhysicianFeeCategory" runat="server" Width="300px"
                                        AutoPostBack="true" OnSelectedIndexChanged="cboSRPhysicianFeeCategory_OnSelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvSRPhysicianFeeCategory" runat="server" ErrorMessage="Physician Fee Category required."
                                        ValidationGroup="entry" ControlToValidate="cboSRPhysicianFeeCategory" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr runat="server" id="trIsProrata">
                                <td class="label"></td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIsProrata" runat="server" Text="Physician Fee Prorata" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr runat="server" id="trIsRemun">
                                <td class="label"></td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIsParamedicFeeRemun" runat="server" Text="Physician Fee Remuneration" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr runat="server" id="trSRPhysicianFeeType">
                                <td class="label">
                                    <asp:Label ID="Label16" runat="server" Text="Physician Fee Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRPhysicianFeeType" runat="server" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Physician Fee Type required."
                                        ValidationGroup="entry" ControlToValidate="cboSRPhysicianFeeCategory" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr runat="server" id="trCoaCostParamedic">
                                <td class="label">
                                    <asp:Label ID="Label14" runat="server" Text="COA Cost Paramedic Fee"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdCostParamedicFee" Width="300px"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboChartOfAccountIdCostParamedicFee_SelectedIndexChanged"
                                        OnItemDataBound="cboCoa_ItemDataBound" OnItemsRequested="cboCoa_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr runat="server" id="trSLCostParamedic">
                                <td class="label">
                                    <asp:Label ID="Label15" runat="server" Text="Subledger Cost Paramedic Fee"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSubledgerIdCostParamedicFee" Width="300px"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSubledgerIdCostParamedicFee_ItemDataBound" OnItemsRequested="cboSubledgerIdCostParamedicFee_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label11" runat="server" Text="Term Of Payment*" ToolTip="If value = 0 then value of term of payment will be taken from App Parameter named InvoiceTermOfPayment"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtTOP" runat="server" Width="100px" MinValue="0"
                                        NumberFormat-DecimalDigits="0" />&nbsp; Day(s)
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblVirtualAccountBank" runat="server" Text="Virtual Account Bank"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtVirtualAccountBank" runat="server" Width="300" MaxLength="50" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblVirtualAccountNo" runat="server" Text="Virtual Account No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtVirtualAccountNo" runat="server" Width="300px" MaxLength="50" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblVirtualAccountName" runat="server" Text="Virtual Account Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtVirtualAccountName" runat="server" Width="100%" MaxLength="200" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgContractSum" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblContractSummary" runat="server" Text="Contract Summary"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtContractSummary" runat="server" Width="600px" TextMode="MultiLine"
                                        Height="500px" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgRelatedComps" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label23" runat="server" Text="Related Companies"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtNoteCompanyList" runat="server" Width="600px" TextMode="MultiLine"
                                        Height="500px" MaxLength="1000" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>

    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1">
        <Tabs>
            <telerik:RadTab runat="server" Text="Address" Selected="True" PageViewID="pgAddress">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Coverage Rule" PageViewID="pgGuarantorRule">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="COA" PageViewID="pgCOA">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Bridging & Integration" PageViewID="pgvAliasName" />
            <telerik:RadTab runat="server" Text="Prescription Location" PageViewID="pgPresc">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Item Restrictions" PageViewID="pgItemRestrictions" />
            <telerik:RadTab runat="server" Text="Item Product Group Margin" PageViewID="pgItemGroupMargin" />
            <telerik:RadTab runat="server" Text="Recipe Amount" PageViewID="pgGuarantorRecipeAmount" />
            <telerik:RadTab runat="server" Text="Plafond (Outpatient)" PageViewID="pgPlafond" />
            <telerik:RadTab runat="server" Text="Auto Bill Item" PageViewID="pgAutoBill" />
            <telerik:RadTab runat="server" Text="Document Checklist" PageViewID="pgDocumentChecklist" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgAddress" runat="server">
            <uc1:Address ID="ctlAddress" runat="server" />
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgGuarantorRule" runat="server">
            <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage2">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Item Type Rule" Selected="True" PageViewID="pgItemTypeRule">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Item Rule" PageViewID="pgItemRule">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Item Prescription Rule" PageViewID="pgItemPrescriptionRule">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Item Prescription Rule (by Therapy Group)" PageViewID="pgItemPrescriptionByTherapyRule">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Import Data (Item Rule & Item Prescription Rule)"
                        PageViewID="pgImport">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Unit Rule" PageViewID="pgUnitRule">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="RadMultiPage2" runat="server" BorderStyle="Solid" SelectedIndex="0"
                BorderColor="Gray">
                <telerik:RadPageView ID="pgItemTypeRule" runat="server">
                    <telerik:RadGrid ID="grdItemTypeRule" runat="server" AutoGenerateColumns="False"
                        OnNeedDataSource="grdItemTypeRule_NeedDataSource" GridLines="None">
                        <MasterTableView DataKeyNames="GuarantorID,ItemID">
                            <Columns>
                                <telerik:GridBoundColumn DataField="GuarantorID" UniqueName="GuarantorID" SortExpression="GuarantorID"
                                    Visible="False" />
                                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                                    SortExpression="ItemID">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                    SortExpression="ItemName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IsToGuarantor" UniqueName="IsToGuarantor" SortExpression="IsToGuarantor"
                                    Visible="False" />
                                <telerik:GridTemplateColumn HeaderText="Rule Setting" UniqueName="RuleSetting">
                                    <HeaderStyle Width="300px" />
                                    <ItemTemplate>
                                        <input id="Radio1" type="radio" runat="server" checked='<%# DataBinder.Eval(Container.DataItem, "IsToGuarantor") %>'>To
                                            Guarantor</input>
                                        <input id="Radio2" type="radio" runat="server" checked='<%# !Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsToGuarantor")) %>'>To
                                            Patient</input>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="True">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgItemRule" runat="server">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="Label9" runat="server" Text="Item ID / Item Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFilterGIRule" runat="server" Width="300px" MaxLength="100" />
                                        </td>
                                        <td width="20">
                                            <asp:ImageButton ID="btnFilter" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                OnClick="btnFilter_Click" ToolTip="Search" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="grdGuarantorItemRule" runat="server" OnNeedDataSource="grdGuarantorItemRule_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdGuarantorItemRule_UpdateCommand"
                        OnDeleteCommand="grdGuarantorItemRule_DeleteCommand" OnInsertCommand="grdGuarantorItemRule_InsertCommand"
                        AllowPaging="true" PageSize="50" OnDetailTableDataBind="grdGuarantorItemRule_DetailTableDataBind">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView DataKeyNames="GuarantorID,ItemID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                                    SortExpression="ItemID">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                    SortExpression="ItemName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="IsInclude" HeaderText="Include" UniqueName="IsInclude"
                                    SortExpression="IsInclude">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn DataField="IsToGuarantor" HeaderText="To Guarantor" UniqueName="IsToGuarantor"
                                    SortExpression="IsToGuarantor">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridBoundColumn DataField="GuarantorRuleTypeName" HeaderText="Rule Type Name"
                                    UniqueName="GuarantorRuleTypeName" SortExpression="GuarantorRuleTypeName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="IsValueInPercent" HeaderText="In Percent"
                                    UniqueName="IsValueInPercent" SortExpression="IsValueInPercent">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridBoundColumn DataField="AmountValue" HeaderText="Amount Value (IPR/Default)"
                                    UniqueName="AmountValue" SortExpression="AmountValue" DataFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="OutpatientAmountValue" HeaderText="Amount Value (OPR)"
                                    UniqueName="OutpatientAmountValue" SortExpression="OutpatientAmountValue" DataFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmergencyAmountValue" HeaderText="Amount Value (EMR)"
                                    UniqueName="EmergencyAmountValue" SortExpression="EmergencyAmountValue" DataFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="IsByTariffComponent" HeaderText="By Tariff Component"
                                    UniqueName="IsByTariffComponent" SortExpression="IsByTariffComponent" Visible="False">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <DetailTables>
                                <telerik:GridTableView Name="detail" DataKeyNames="GuarantorID,ItemID,TariffComponentID"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Tariff Component Name"
                                            UniqueName="TariffComponentName" SortExpression="TariffComponentName">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn DataField="AmountValue" UniqueName="AmountValue" SortExpression="AmountValue"
                                            HeaderText="IPR/Default" DataFormatString="{0:n2}">
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="OutpatientAmountValue" UniqueName="OutpatientAmountValue"
                                            HeaderText="OPR" SortExpression="OutpatientAmountValue" DataFormatString="{0:n2}">
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="EmergencyAmountValue" UniqueName="EmergencyAmountValue"
                                            HeaderText="EMR" SortExpression="EmergencyAmountValue" DataFormatString="{0:n2}">
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridNumericColumn>
                                    </Columns>
                                </telerik:GridTableView>
                            </DetailTables>
                            <EditFormSettings UserControlName="GuarantorItemRuleDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="GuarantorItemRuleEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="True">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgItemPrescriptionRule" runat="server">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="Label10" runat="server" Text="Item ID / Item Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFilterGIPRule" runat="server" Width="300px" MaxLength="100" />
                                        </td>
                                        <td width="20">
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                OnClick="btnFilter_Click" ToolTip="Search" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="grdGuarantorItemPrescriptionRule" runat="server" OnNeedDataSource="grdGuarantorItemPrescriptionRule_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdGuarantorItemPrescriptionRule_UpdateCommand"
                        OnDeleteCommand="grdGuarantorItemPrescriptionRule_DeleteCommand" OnInsertCommand="grdGuarantorItemPrescriptionRule_InsertCommand"
                        AllowPaging="true" PageSize="50">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView DataKeyNames="GuarantorID,ItemID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                                    SortExpression="ItemID">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                    SortExpression="ItemName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="IsInclude" HeaderText="Include" UniqueName="IsInclude"
                                    SortExpression="IsInclude">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn DataField="IsToGuarantor" HeaderText="To Guarantor" UniqueName="IsToGuarantor"
                                    SortExpression="IsToGuarantor">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridBoundColumn DataField="GuarantorRuleTypeName" HeaderText="Rule Type Name"
                                    UniqueName="GuarantorRuleTypeName" SortExpression="GuarantorRuleTypeName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="IsValueInPercent" HeaderText="In Percent"
                                    UniqueName="IsValueInPercent" SortExpression="IsValueInPercent">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridBoundColumn DataField="AmountValue" HeaderText="Amount Value (IPR/Default)"
                                    UniqueName="AmountValue" SortExpression="AmountValue" DataFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="OutpatientAmountValue" HeaderText="Amount Value (OPR)"
                                    UniqueName="OutpatientAmountValue" SortExpression="OutpatientAmountValue" DataFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmergencyAmountValue" HeaderText="Amount Value (EMR)"
                                    UniqueName="EmergencyAmountValue" SortExpression="EmergencyAmountValue" DataFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="GuarantorItemPrescriptionRuleDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="GuarantorItemPrescriptionRuleEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="True">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgItemPrescriptionByTherapyRule" runat="server">
                    <telerik:RadGrid ID="grdGuarantorItemPrescriptionByTherapyRule" runat="server" OnNeedDataSource="grdGuarantorItemPrescriptionByTherapyRule_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdGuarantorItemPrescriptionByTherapyRule_UpdateCommand"
                        OnDeleteCommand="grdGuarantorItemPrescriptionByTherapyRule_DeleteCommand" OnInsertCommand="grdGuarantorItemPrescriptionByTherapyRule_InsertCommand"
                        AllowPaging="true" PageSize="50">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView DataKeyNames="GuarantorID,SRTherapyGroup">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="SRTherapyGroup" HeaderText="ID" UniqueName="SRTherapyGroup"
                                    SortExpression="SRTherapyGroup" Visible="False">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TherapyGroupName" HeaderText="Therapy Group" UniqueName="TherapyGroupName"
                                    SortExpression="TherapyGroupName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="IsInclude" HeaderText="Include" UniqueName="IsInclude"
                                    SortExpression="IsInclude">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn DataField="IsToGuarantor" HeaderText="To Guarantor" UniqueName="IsToGuarantor"
                                    SortExpression="IsToGuarantor">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridBoundColumn DataField="GuarantorRuleTypeName" HeaderText="Rule Type Name"
                                    UniqueName="GuarantorRuleTypeName" SortExpression="GuarantorRuleTypeName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="IsValueInPercent" HeaderText="In Percent"
                                    UniqueName="IsValueInPercent" SortExpression="IsValueInPercent">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridBoundColumn DataField="AmountValue" HeaderText="Amount Value (IPR/Default)"
                                    UniqueName="AmountValue" SortExpression="AmountValue" DataFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="OutpatientAmountValue" HeaderText="Amount Value (OPR)"
                                    UniqueName="OutpatientAmountValue" SortExpression="OutpatientAmountValue" DataFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmergencyAmountValue" HeaderText="Amount Value (EMR)"
                                    UniqueName="EmergencyAmountValue" SortExpression="EmergencyAmountValue" DataFormatString="{0:n2}">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="GuarantorItemPrescriptionByTherapyRuleDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="GuarantorItemPrescriptionTheraoyRuleEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="True">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgImport" runat="server">
                    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
                        BorderColor="#FFC080" BorderStyle="Solid">
                        <table width="100%">
                            <tr>
                                <td width="10px" valign="top">
                                    <asp:Image ID="Image7" ImageUrl="~/Images/boundleft.gif" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="50%" style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRule" runat="server" Text="Rule"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboImportRuleId" runat="server" Width="300px" />
                                        </td>
                                        <td style="width: 20px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50%" style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblImportItemType" runat="server" Text="Item Type"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboImportItemType" runat="server" Width="300px" AutoPostBack="True"
                                                OnSelectedIndexChanged="cboImportItemType_SelectedIndexChanged" />
                                        </td>
                                        <td style="width: 20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblImportItemGroup" runat="server" Text="Item Group"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboImportItemGroup" runat="server" Width="300px" AllowCustomText="true"
                                                Filter="Contains" />
                                        </td>
                                        <td style="width: 20px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td class="labelcaption">
                                            <asp:Label ID="lblRules" runat="server" Text="Item Rule"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="50%" style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label"></td>
                                        <td class="entry">
                                            <asp:RadioButtonList ID="rblImportInclude" runat="server" RepeatDirection="Horizontal"
                                                AutoPostBack="true" OnSelectedIndexChanged="rblImportInclude_SelectedIndexChanged">
                                                <asp:ListItem Selected="True">Include</asp:ListItem>
                                                <asp:ListItem>Exclude</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label"></td>
                                        <td class="entry">
                                            <asp:RadioButtonList ID="rblImportToGuarantor" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True">To Guarantor</asp:ListItem>
                                                <asp:ListItem>To Patient</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label"></td>
                                        <td class="entry">
                                            <asp:Button ID="btnImport" Text="Import" runat="server" OnClick="btnImport_Click"></asp:Button>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top" width="100%">
                                <table width="100%" id="tblRuleType" runat="server">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblImportRuleTypeName" runat="server" Text="Rule Type Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadComboBox ID="cboImportSRGuarantorRuleType" runat="server" Width="200px" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkImportIsValueInPercent" runat="server" Text="In Percent" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <table width="100%">
                                                <tr>
                                                    <td class="label" style="width: 33%">
                                                        <asp:Label ID="Label1" runat="server" Text="IPR/Default"></asp:Label>
                                                    </td>
                                                    <td class="label" style="width: 33%">
                                                        <asp:Label ID="Label5" runat="server" Text="OPR"></asp:Label>
                                                    </td>
                                                    <td class="label" style="width: 33%">
                                                        <asp:Label ID="Label6" runat="server" Text="EMR"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblImportAmountValue" runat="server" Text="Amount Value"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table width="100%">
                                                <tr>
                                                    <td class="entry" style="width: 33%">
                                                        <telerik:RadNumericTextBox ID="txtImportAmountValue" runat="server" Width="100px" />
                                                    </td>
                                                    <td class="entry" style="width: 33%">
                                                        <telerik:RadNumericTextBox ID="txtImportOutpatientAmountValue" runat="server" Width="100px" />
                                                    </td>
                                                    <td class="entry" style="width: 33%">
                                                        <telerik:RadNumericTextBox ID="txtImportEmergencyAmountValue" runat="server" Width="100px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgUnitRule" runat="server">
                    <telerik:RadGrid ID="grdUnitRule" runat="server" OnNeedDataSource="grdUnitRule_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdUnitRule_UpdateCommand"
                        OnDeleteCommand="grdUnitRule_DeleteCommand" OnInsertCommand="grdUnitRule_InsertCommand">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView DataKeyNames="GuarantorID,ServiceUnitID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="ServiceUnitID" HeaderText="ID" UniqueName="ServiceUnitID"
                                    SortExpression="ServiceUnitID">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                                    SortExpression="ServiceUnitName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="IsCovered" HeaderText="Covered" UniqueName="IsCovered"
                                    SortExpression="IsValueInPercent">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="GuarantorServiceUnitRulesDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="GuarantorServiceUnitRulesDetailEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="True">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </telerik:RadPageView>

        <telerik:RadPageView ID="pgCOA" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" style="vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">Account Group
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboAccountGroupID" runat="server" Width="300px" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvIncomeGroup" runat="server" ErrorMessage="Account Group required."
                                        ValidationGroup="entry" ControlToValidate="cboAccountGroupID" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>

                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountId" runat="server" Text="COA (A/R Invoice) - OPR"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboChartOfAccountId" Width="300px" EnableLoadOnDemand="true"
                                        HighlightTemplatedItems="true" AutoPostBack="true" OnSelectedIndexChanged="cboChartOfAccountId_SelectedIndexChanged"
                                        OnItemDataBound="cboChartOfAccountId_ItemDataBound" OnItemsRequested="cboChartOfAccountId_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvChartOfAccountId" runat="server" ErrorMessage="Chart Of Account required."
                                        ValidationGroup="entry" ControlToValidate="cboChartOfAccountId" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>

                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerId" runat="server" Text="Subledger"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSubledgerId" Width="300px" EnableLoadOnDemand="true"
                                        HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSubledgerId_ItemDataBound"
                                        OnItemsRequested="cboSubledgerId_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>

                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdTemporary" runat="server" Text="COA (A/R Process) - OPR"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdTemporary" Width="300px"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboChartOfAccountIdTemporary_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdTemporary_ItemDataBound"
                                        OnItemsRequested="cboChartOfAccountIdTemporary_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>

                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdTemporary" runat="server" Text="Subledger"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSubledgerIdTemporary" Width="300px" EnableLoadOnDemand="true"
                                        HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSubledgerIdTemporary_ItemDataBound"
                                        OnItemsRequested="cboSubledgerIdTemporary_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>


                            <%-- Edited by Fajri --%>
                            <tr runat="server" id="trCoaIpr">
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdIPR" runat="server" Text="COA (A/R Invoice) - IPR"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdIPR" Width="300px" EnableLoadOnDemand="true"
                                        HighlightTemplatedItems="true" AutoPostBack="true" OnSelectedIndexChanged="cboChartOfAccountIdIPR_SelectedIndexChanged"
                                        OnItemDataBound="cboChartOfAccountIdIPR_ItemDataBound" OnItemsRequested="cboChartOfAccountIdIPR_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvChartOfAccountIdIPR" runat="server" ErrorMessage="Chart Of Account required."
                                        ValidationGroup="entry" ControlToValidate="cboChartOfAccountIdIPR" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>

                            <tr runat="server" id="trSlIpr">
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdIPR" runat="server" Text="Subledger"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSubledgerIdIPR" Width="300px" EnableLoadOnDemand="true"
                                        HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSubledgerIdIPR_ItemDataBound"
                                        OnItemsRequested="cboSubledgerIdIPR_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <%-- Edited by Fajri --%>
                        </table>
                    </td>
                    <td width="50%" style="vertical-align: top">
                        <table width="100%">
                            <%-- Edited by Fajri --%>
                            <tr runat="server" id="trCoaTempIpr">
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdTemporaryIPR" runat="server" Text="COA (A/R Process) - IPR"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdTemporaryIPR" Width="300px"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboChartOfAccountIdTemporaryIPR_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdTemporaryIPR_ItemDataBound"
                                        OnItemsRequested="cboChartOfAccountIdTemporaryIPR_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>

                            <tr runat="server" id="trSlTempIpr">
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdTemporaryIPR" runat="server" Text="Subledger"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSubledgerIdTemporaryIPR" Width="300px" EnableLoadOnDemand="true"
                                        HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSubledgerIdTemporaryIPR_ItemDataBound"
                                        OnItemsRequested="cboSubledgerIdTemporaryIPR_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <%-- Edited by Fajri --%>

                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label7" runat="server" Text="COA Deposit"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdDeposit" Width="300px"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboChartOfAccountIdDeposit_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdDeposit_ItemDataBound"
                                        OnItemsRequested="cboChartOfAccountIdDeposit_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label8" runat="server" Text="Subledger"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSubledgerIdDeposit" Width="300px" EnableLoadOnDemand="true"
                                        HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSubledgerIdDeposit_ItemDataBound"
                                        OnItemsRequested="cboSubledgerIdDeposit_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label12" runat="server" Text="COA Exccess Payment / Discount (A/R)* (+)"
                                        ToolTip="Exccess claim A/R un-invoice guarantor, discount of A/R Payment, exccess payment of A/R Payment"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdOverPayment" Width="300px"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboChartOfAccountIdOverPayment_SelectedIndexChanged"
                                        OnItemDataBound="cboChartOfAccountIdOverPayment_ItemDataBound" OnItemsRequested="cboChartOfAccountIdOverPayment_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label13" runat="server" Text="Subledger"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSubledgerIdOverPayment" Width="300px"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSubledgerIdOverPayment_ItemDataBound" OnItemsRequested="cboSubledgerIdOverPayment_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label24" runat="server" Text="COA Exccess Payment / Discount (A/R)* (-)"
                                        ToolTip="Exccess claim A/R un-invoice guarantor, discount of A/R Payment, exccess payment of A/R Payment"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdOverPaymentMin" Width="300px"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboChartOfAccountIdOverPaymentMin_SelectedIndexChanged"
                                        OnItemDataBound="cboChartOfAccountIdOverPaymentMin_ItemDataBound" OnItemsRequested="cboChartOfAccountIdOverPaymentMin_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label25" runat="server" Text="Subledger"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSubledgerIdOverPaymentMin" Width="300px"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSubledgerIdOverPaymentMin_ItemDataBound" OnItemsRequested="cboSubledgerIdOverPaymentMin_ItemsRequested">
                                        <ItemTemplate>
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                            </b>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
        </telerik:RadPageView>

        <telerik:RadPageView runat="server" ID="pgvAliasName">
            <telerik:RadGrid ID="grdAliasName" runat="server" OnNeedDataSource="grdAliasName_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdAliasName_UpdateCommand"
                OnDeleteCommand="grdAliasName_DeleteCommand" OnInsertCommand="grdAliasName_InsertCommand"
                AllowPaging="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="GuarantorID, SRBridgingType"
                    PageSize="15">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="BridgingTypeName" HeaderText="Bridging Type"
                            UniqueName="BridgingTypeName" SortExpression="BridgingTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="BridgingID" HeaderText="Bridging ID" UniqueName="BridgingID"
                            SortExpression="BridgingID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="BridgingCode" HeaderText="Bridging Code" UniqueName="BridgingCode"
                            SortExpression="BridgingCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CoverageValue" HeaderText="Coverage Value (%)"
                            UniqueName="CoverageValue" SortExpression="CoverageValue" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MarginValue" HeaderText="Margin Value (%)"
                            UniqueName="MarginValue" SortExpression="MarginValue" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="GuarantorAliasDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="GuarantorAliasEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>

        <telerik:RadPageView runat="server" ID="pgPresc">
            <table>
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label20" runat="server" Text="Pharmacy Service Unit for Inpatient"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboPSuIPR" AutoPostBack="true" OnSelectedIndexChanged="cboPharmacyServiceUnitID_SelectedIndexChanged"
                                        runat="server" Width="300px" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label21" runat="server" Text="Pharmacy Service Unit for Outpatient"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboPSuOPR" AutoPostBack="true" OnSelectedIndexChanged="cboPharmacyServiceUnitID_SelectedIndexChanged"
                                        runat="server" Width="300px" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label22" runat="server" Text="Pharmacy Service Unit for Emergency"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboPSuEMR" AutoPostBack="true" OnSelectedIndexChanged="cboPharmacyServiceUnitID_SelectedIndexChanged"
                                        runat="server" Width="300px" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label17" runat="server" Text="Pharmacy Location for Inpatient"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboPLocIPR" runat="server" Width="300px" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label18" runat="server" Text="Pharmacy Location Outpatient"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboPLocOPR" runat="server" Width="300px" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label19" runat="server" Text="Pharmacy Location Emergency"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboPLocEMR" runat="server" Width="300px" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>

        <telerik:RadPageView runat="server" ID="pgItemRestrictions">
            <telerik:RadTabStrip ID="RadTabStrip3" runat="server" MultiPageID="RadMultiPage3">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Item Service" Selected="True" PageViewID="pgItemServiceRestrictions">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Item Laboratory" PageViewID="pgItemLabRestrictions">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Item Radiology" PageViewID="pgItemRadRestrictions">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Item Product" PageViewID="pgItemProductRestrictions">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="RadMultiPage3" runat="server" BorderStyle="Solid" SelectedIndex="0"
                BorderColor="Gray">
                <telerik:RadPageView ID="pgItemServiceRestrictions" runat="server">
                    <table>
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblIsItemServiceRestrictionStatusAllowed" runat="server" Text="Restriction Status"></asp:Label></td>
                                        <td class="entry">
                                            <asp:RadioButtonList ID="rbtIsItemServiceRestrictionStatusAllowed" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Value="1" Text="Allowed" />
                                                <asp:ListItem Value="0" Text="Not Allowed" />
                                            </asp:RadioButtonList>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvIsItemServiceRestrictionStatusAllowed" runat="server" ErrorMessage="Item Service Restriction Status required."
                                                ValidationGroup="entry" ControlToValidate="rbtIsItemServiceRestrictionStatusAllowed" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                </table>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="grdGuarantorItemServiceRestrictions" runat="server" OnNeedDataSource="grdGuarantorItemServiceRestrictions_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdGuarantorItemServiceRestrictions_DeleteCommand"
                        OnInsertCommand="grdGuarantorItemServiceRestrictions_InsertCommand" AllowPaging="true"
                        PageSize="50">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView DataKeyNames="GuarantorID,ItemID">
                            <%--<CommandItemTemplate>
                                <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdGuarantorItemServiceRestrictions.MasterTableView.IsItemInserted %>'>
                                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                                    &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                                </asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="lbPickList" runat="server" Visible='<%# !grdGuarantorItemServiceRestrictions.MasterTableView.IsItemInserted %>'
                                    OnClientClick="javascript:openWinPickList('01');return false;">
                                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />
                                    &nbsp;<asp:Label runat="server" ID="lblPicList" Text="Item picker"></asp:Label>
                                </asp:LinkButton>
                            </CommandItemTemplate>
                            <CommandItemStyle Height="29px" />--%>
                            <Columns>
                                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                                    SortExpression="ItemID">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                    SortExpression="ItemName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="GuarantorItemRestrictionsServiceDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="GuarantorItemRestrictionsServiceEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="True">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgItemLabRestrictions" runat="server">
                    <table>
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblIsItemLabRestrictionStatusAllowed" runat="server" Text="Restriction Status"></asp:Label></td>
                                        <td class="entry">
                                            <asp:RadioButtonList ID="rbtIsItemLabRestrictionStatusAllowed" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Value="1" Text="Allowed" />
                                                <asp:ListItem Value="0" Text="Not Allowed" />
                                            </asp:RadioButtonList>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvIsItemLabRestrictionStatusAllowed" runat="server" ErrorMessage="Item Laboratory Restriction Status required."
                                                ValidationGroup="entry" ControlToValidate="rbtIsItemLabRestrictionStatusAllowed" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                </table>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="grdGuarantorItemLabRestrictions" runat="server" OnNeedDataSource="grdGuarantorItemLabRestrictions_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdGuarantorItemLabRestrictions_DeleteCommand"
                        OnInsertCommand="grdGuarantorItemLabRestrictions_InsertCommand" AllowPaging="true"
                        PageSize="50">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView DataKeyNames="GuarantorID,ItemID">
                            <Columns>
                                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                                    SortExpression="ItemID">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                    SortExpression="ItemName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="GuarantorItemRestrictionsLaboratoryDetail.ascx"
                                EditFormType="WebUserControl">
                                <EditColumn UniqueName="GuarantorItemRestrictionsLaboratoryEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="True">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgItemRadRestrictions" runat="server">
                    <table>
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblIsItemRadRestrictionStatusAllowed" runat="server" Text="Restriction Status"></asp:Label></td>
                                        <td class="entry">
                                            <asp:RadioButtonList ID="rbtIsItemRadRestrictionStatusAllowed" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Value="1" Text="Allowed" />
                                                <asp:ListItem Value="0" Text="Not Allowed" />
                                            </asp:RadioButtonList>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvIsItemRadRestrictionStatusAllowed" runat="server" ErrorMessage="Item Radiology Restriction Status required."
                                                ValidationGroup="entry" ControlToValidate="rbtIsItemRadRestrictionStatusAllowed" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image17" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                </table>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="grdGuarantorItemRadRestrictions" runat="server" OnNeedDataSource="grdGuarantorItemRadRestrictions_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdGuarantorItemRadRestrictions_DeleteCommand"
                        OnInsertCommand="grdGuarantorItemRadRestrictions_InsertCommand" AllowPaging="true"
                        PageSize="50">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView DataKeyNames="GuarantorID,ItemID">
                            <Columns>
                                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                                    SortExpression="ItemID">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                    SortExpression="ItemName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="GuarantorItemRestrictionsRadiologyDetail.ascx"
                                EditFormType="WebUserControl">
                                <EditColumn UniqueName="GuarantorItemRestrictionsRadiologyEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="True">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgItemProductRestrictions" runat="server">
                    <table>
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblTypeOfDrug" runat="server" Text="Type Of Drug"></asp:Label></td>
                                        <td class="entry" colspan="3">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsItemRestrictionsFornas" runat="server" Text="National Formulary" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsItemRestrictionsFormularium" runat="server" Text="Formulary" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsItemRestrictionsGeneric" runat="server" Text="Generic" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsItemRestrictionsNonGenericLimited" runat="server" Text="Non Generic Limited" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIsItemRestrictionsNonGeneric" runat="server" Text="Non Generic" />
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                </table>
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <table>
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblIsItemProductRestrictionStatusAllowed" runat="server" Text="Restriction Status"></asp:Label></td>
                                        <td class="entry">
                                            <asp:RadioButtonList ID="rbtIsItemProductRestrictionStatusAllowed" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Value="1" Text="Allowed" />
                                                <asp:ListItem Value="0" Text="Not Allowed" />
                                            </asp:RadioButtonList>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvIsItemProductRestrictionStatusAllowed" runat="server" ErrorMessage="Item Product Restriction Status required."
                                                ValidationGroup="entry" ControlToValidate="rbtIsItemProductRestrictionStatusAllowed" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image18" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                </table>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="grdGuarantorItemRestrictions" runat="server" OnNeedDataSource="grdGuarantorItemRestrictions_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdGuarantorItemRestrictions_DeleteCommand"
                        OnInsertCommand="grdGuarantorItemRestrictions_InsertCommand" AllowPaging="true"
                        PageSize="50">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView DataKeyNames="GuarantorID,ItemID">
                            <Columns>
                                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                                    SortExpression="ItemID">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                    SortExpression="ItemName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="GuarantorItemRestrictionsDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="GuarantorItemRestrictionsEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="True">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </telerik:RadPageView>

        <telerik:RadPageView runat="server" ID="pgItemGroupMargin">
            <table width="100%">
                <tr>
                    <td style="width: 100%">
                        <telerik:RadGrid ID="grdItemProductGroupMargin" runat="server" OnNeedDataSource="grdItemProductGroupMargin_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemProductGroupMargin_UpdateCommand"
                            OnDeleteCommand="grdItemProductGroupMargin_DeleteCommand" OnInsertCommand="grdItemProductGroupMargin_InsertCommand"
                            AllowPaging="true" PageSize="50">
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView DataKeyNames="GuarantorID,ItemGroupID">
                                <Columns>
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle CssClass="MyImageButton" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn DataField="ItemTypeName" HeaderText="Item Type"
                                        UniqueName="ItemTypeName" SortExpression="RegistrationType" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="150px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ItemGroupName" HeaderText="Item Group"
                                        UniqueName="ItemGroupName" SortExpression="ItemGroupName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MarginPercentage" HeaderText="Margin (%)"
                                        UniqueName="MarginPercentage" SortExpression="MarginPercentage" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                    <telerik:GridBoundColumn DataField="MarginName" HeaderText="Margin Group"
                                        UniqueName="MarginName" SortExpression="ItemGroupName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn />
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <EditFormSettings UserControlName="GuarantorItemGroupProductMarginDetail.ascx" EditFormType="WebUserControl">
                                    <EditColumn UniqueName="GuarantorItemGroupProductMarginDetailEditCommand">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                            <FilterMenu>
                            </FilterMenu>
                            <ClientSettings EnableRowHoverStyle="True">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>

        <telerik:RadPageView runat="server" ID="pgGuarantorRecipeAmount">
            <table>
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label26" runat="server" Text="Default Recipe Amount"></asp:Label></td>
                                <td class="entry" colspan="3">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIsUsingDefaultRecipeAmount" runat="server" Text="Using Default Recipe Amount" />
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRecipeMargin" runat="server" Text="Recipe Margin Value Non Compound"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="entry" style="width: 33%">
                                                <telerik:RadNumericTextBox ID="txtRecipeMarginValueNonCompound" runat="server" Width="100px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdGuarantorRecipeAmount" runat="server" OnNeedDataSource="grdGuarantorRecipeAmount_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdGuarantorRecipeAmount_DeleteCommand"
                OnInsertCommand="grdGuarantorRecipeAmount_InsertCommand" OnUpdateCommand="grdGuarantorRecipeAmount_UpdateCommand"
                AllowPaging="true"
                PageSize="50">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView DataKeyNames="GuarantorID,CounterID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="StartingValue" HeaderText="Starting Value" UniqueName="StartingValue"
                            SortExpression="StartingValue" DataFormatString="{0:n0}">
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EndingValue" HeaderText="Ending Value" UniqueName="EndingValue"
                            SortExpression="EndingValue" DataFormatString="{0:n0}">
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RecipeAmount" HeaderText="Recipe Margin Value Compound" UniqueName="RecipeAmount"
                            SortExpression="RecipeAmount" DataFormatString="{0:n0}">
                            <HeaderStyle HorizontalAlign="Right" Width="200px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="GuarantorRecipeAmountDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="GuarantorRecipeAmountEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>

        <telerik:RadPageView runat="server" ID="pgPlafond">
            <table width="100%">
                <tr>
                    <td style="width: 100%">
                        <telerik:RadGrid ID="grdPlafond" runat="server" OnNeedDataSource="grdPlafond_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPlafond_UpdateCommand"
                            OnDeleteCommand="grdPlafond_DeleteCommand" OnInsertCommand="grdPlafond_InsertCommand"
                            AllowPaging="true" PageSize="50">
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView DataKeyNames="GuarantorID,ServiceUnitID">
                                <Columns>
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle CssClass="MyImageButton" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit"
                                        UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="350px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="PlafondAmount" HeaderText="Plafond Amount"
                                        UniqueName="PlafondAmount" SortExpression="PlafondAmount" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                    <telerik:GridTemplateColumn />
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <EditFormSettings UserControlName="GuarantorServiceUnitPlafondDetail.ascx" EditFormType="WebUserControl">
                                    <EditColumn UniqueName="GuarantorServiceUnitPlafondDetailEditCommand">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                            <FilterMenu>
                            </FilterMenu>
                            <ClientSettings EnableRowHoverStyle="True">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>

        <telerik:RadPageView runat="server" ID="pgAutoBill">
            <table width="100%">
                <tr>
                    <td style="width: 100%">
                        <telerik:RadGrid ID="grdAutoBillItem" runat="server" OnNeedDataSource="grdAutoBillItem_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdAutoBillItem_UpdateCommand"
                            OnDeleteCommand="grdAutoBillItem_DeleteCommand" OnInsertCommand="grdAutoBillItem_InsertCommand"
                            AllowPaging="true" PageSize="50">
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView DataKeyNames="GuarantorID,ServiceUnitID,ItemID">
                                <ColumnGroups>
                                    <telerik:GridColumnGroup HeaderText="Generate On" Name="Generate" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridColumnGroup>
                                </ColumnGroups>
                                <Columns>
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle CssClass="MyImageButton" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                                        SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                        UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                        SortExpression="ItemName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="Quantity" HeaderText="Qty"
                                        UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemUnit" HeaderText="Unit"
                                        UniqueName="ItemUnit" SortExpression="ItemUnit" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="120px" DataField="IsGenerateOnRegistration"
                                        HeaderText="Old Patient Reg." UniqueName="IsGenerateOnRegistration"
                                        SortExpression="IsGenerateOnRegistration" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" ColumnGroupName="Generate" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="120px" DataField="IsGenerateOnNewRegistration"
                                        HeaderText="New Patient Reg." UniqueName="IsGenerateOnNewRegistration"
                                        SortExpression="IsGenerateOnNewRegistration" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" ColumnGroupName="Generate" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="120px" DataField="IsGenerateOnReferral"
                                        HeaderText="Referral" UniqueName="IsGenerateOnReferral" SortExpression="IsGenerateOnReferral"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ColumnGroupName="Generate" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="120px" DataField="IsGenerateOnFirstRegistration"
                                        HeaderText="1# Registration" UniqueName="IsGenerateOnFirstRegistration"
                                        SortExpression="IsGenerateOnFirstRegistration" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" ColumnGroupName="Generate" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                                        UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                        <HeaderStyle Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <EditFormSettings UserControlName="GuarantorAutoBillItemDetail.ascx" EditFormType="WebUserControl">
                                    <EditColumn UniqueName="GuarantorAutoBillItemDetailEditCommand">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                            <FilterMenu>
                            </FilterMenu>
                            <ClientSettings EnableRowHoverStyle="True">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>

        <telerik:RadPageView runat="server" ID="pgDocumentChecklist">
            <telerik:RadGrid ID="grdDocumentChecklist" runat="server" OnNeedDataSource="grdDocumentChecklist_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdDocumentChecklist_DeleteCommand"
                OnInsertCommand="grdDocumentChecklist_InsertCommand" AllowPaging="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="GuarantorID, SRRegistrationType"
                    PageSize="15">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="RegistrationType" HeaderText="Registration Type"
                            UniqueName="RegistrationType" SortExpression="RegistrationType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle Width="200px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DocumentChecklistName" HeaderText="Document Checklist" UniqueName="DocumentChecklistName"
                            SortExpression="DocumentChecklistName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="GuarantorDocumentChecklistDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="GuarantorDocumentChecklistEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
