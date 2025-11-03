<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="PhysicianLeaveDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PhysicianLeaveDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20"
                                Enabled="false" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                MarkFirstMatch="true" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboParamedicID_ItemDataBound"
                                OnItemsRequested="cboParamedicID_ItemsRequested" AutoPostBack="True" OnSelectedIndexChanged="cboParamedicID_SelectedIndexChanged">
                                <FooterTemplate>
                                    Note : Show max 20 result
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ErrorMessage="Physician required."
                                ValidationGroup="entry" ControlToValidate="cboParamedicID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblStartDate" runat="server" Text="For Period"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtStartDate" runat="server" Width="100px" />
                                    </td>
                                    <td>&nbsp;&nbsp;to&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtEndDate" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="Start Date required."
                                ValidationGroup="entry" ControlToValidate="txtStartDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ErrorMessage="End Date required."
                                ValidationGroup="entry" ControlToValidate="txtEndDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRPysicianLeaveReason" runat="server" Text="Leave Reason"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRPhysicianLeaveReason" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRPhysicianLeaveReason" runat="server" ErrorMessage="Leave Reason required."
                                ValidationGroup="entry" ControlToValidate="cboSRPhysicianLeaveReason" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="LblSubsParamedicIP" runat="server" Text="Substitute Physician-Inpatient"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSubsParamedicIP" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="LblSubsParamedicOp" runat="server" Text="Substitute Physician-Outpatient"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSubsParamedicOP" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="LblSubsParamedicEMR" runat="server" Text="Physician-On Call"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSubsParamedicEMR" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine"
                                Height="50px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" Enabled="false" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <legend>EXECPTION UNIT</legend>
                    <table width="100%">
                        <tr>
                            <td>
                                <telerik:RadGrid ID="grdExeptionUnit" runat="server" AutoGenerateColumns="False"
                                    GridLines="None" OnNeedDataSource="grdExeptionUnit_NeedDataSource">
                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ServiceUnitID" ClientDataKeyNames="ServiceUnitID">
                                        <Columns>
                                            <%--<telerik:GridTemplateColumn HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="chkIsSelect" Enabled="<%#DataModeCurrent != AppEnum.DataMode.Read %>"
                                                        Checked='<%#DataBinder.Eval(Container.DataItem, "IsSelect") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>--%>
                                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="50px">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                                        runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="chkIsSelect" Enabled="<%#DataModeCurrent != AppEnum.DataMode.Read %>"
                                                        Checked='<%#DataBinder.Eval(Container.DataItem, "IsSelect") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ServiceUnitID" HeaderText="ID"
                                                UniqueName="ServiceUnitID" SortExpression="ServiceUnitID" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ServiceUnitName" HeaderText="Service Unit"
                                                UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Start Time">
                                                <ItemTemplate>
                                                    <telerik:RadMaskedTextBox ID="txtStartTime" runat="server" Enabled="<%#DataModeCurrent != AppEnum.DataMode.Read %>"
                                                        Mask="<00..23>:<00..59>" PromptChar="_" RoundNumericRanges="false" Width="50px"
                                                        Text='<%#DataBinder.Eval(Container.DataItem, "StartTime") %>'>
                                                    </telerik:RadMaskedTextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="End Time">
                                                <ItemTemplate>
                                                    <telerik:RadMaskedTextBox ID="txtEndTime" runat="server" Enabled="<%#DataModeCurrent != AppEnum.DataMode.Read %>"
                                                        Mask="<00..23>:<00..59>" PromptChar="_" RoundNumericRanges="false" Width="50px"
                                                        Text='<%#DataBinder.Eval(Container.DataItem, "EndTime") %>'>
                                                    </telerik:RadMaskedTextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn />
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
