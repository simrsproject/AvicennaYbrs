<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="ParamedicFeeByServiceSettingDetail.aspx.cs" 
    Inherits="Temiang.Avicenna.Module.Finance.Master.ParamedicFeeByServiceSettingDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationType" runat="server" Text="Registration Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRRegistrationType" runat="server" Width="304px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboSRRegistrationType_SelectedIndexChanged" />
                            <asp:HiddenField ID="hfId" runat="server" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnit" runat="server" Width="304px"
                                OnItemDataBound="cboServiceUnit_ItemDataBound"
                                OnItemsRequested="cboServiceUnit_ItemsRequested"
                            >
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label9" runat="server" Text="Item"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItem" runat="server" Width="304px" AutoPostBack="true"
                                EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                                OnItemDataBound="cboItem_ItemDataBound"
                                OnItemsRequested="cboItem_ItemsRequested"
                                OnSelectedIndexChanged="cboItem_SelectedIndexChanged"
                            >
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label10" runat="server" Text="Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboClass" runat="server" Width="304px" 
                                OnSelectedIndexChanged="cboClass_SelectedIndexChanged" AutoPostBack="true">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Physician Fee Case Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRParamedicFeeCaseType" runat="server" Width="304px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Join Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRParamedicFeeIsTeam" runat="server" Width="304px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="Paramedic Fee Team Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRParamedicFeeTeamStatus" runat="server" Width="304px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label6" runat="server" Text="Tarif Component"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboTariffComponent" runat="server" Width="304px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label8" runat="server" Text="Fee Value In Percent"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsFeeValueInPercent" runat="server" Text="Fee Value In Percent" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label7" runat="server" Text="Fee Value"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtFeeValue" runat="server" Type="Number" NumberFormat-DecimalDigits="2"
                                            Width="100px" MinValue="0" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Fee value required."
                                ControlToValidate="txtFeeValue" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Max (0 = Unlimited)"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtCountMax" runat="server" Type="Number" NumberFormat-DecimalDigits="0"
                                            Width="100px" MinValue="0" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Fee value required."
                                ControlToValidate="txtFeeValue" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label11" runat="server" Text="Ignored If Any Replacement"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIgnoredIfAnyReplacement" runat="server" Text="Ignored If Any Replacement" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label12" runat="server" Text="Is Replacement"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsReplacement" runat="server" Text="Is Replacement" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label13" runat="server" Text="Is Replacement For Fee By Percentage Of AR"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsReplacementForFeeByPercentageOfAR" runat="server" Text="Is Replacement For Fee By Percentage Of AR" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
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
</asp:Content>
