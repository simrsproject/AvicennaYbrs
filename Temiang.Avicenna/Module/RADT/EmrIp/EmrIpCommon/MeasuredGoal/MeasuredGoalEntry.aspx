<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogHistEntry.Master" AutoEventWireup="true"
    CodeBehind="MeasuredGoalEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MeasuredGoalEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphList" runat="server">
    <telerik:RadGrid ID="grdMeasuredGoal" runat="server" OnNeedDataSource="grdMeasuredGoal_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdMeasuredGoal_DeleteCommand" Height="400px"
        OnItemCommand="grdMeasuredGoal_ItemCommand" OnItemDataBound="grdMeasuredGoal_ItemDataBound">
        <MasterTableView DataKeyNames="SeqNo,IsVoid">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnView" runat="server" CommandName="View" ToolTip='View'>
                            <img src="../../../../../Images/Toolbar/views16.png" border="0" alt=""/>
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn DataField="SeqNo" UniqueName="SeqNo" HeaderText="No" HeaderStyle-Width="50px" />
                <telerik:GridDateTimeColumn DataField="CreatedDateTime" UniqueName="CreatedDateTime" HeaderText="Date" HeaderStyle-Width="100px" />
                <telerik:GridBoundColumn DataField="CreatedByUserName" UniqueName="CreatedByUserName" HeaderText="PPA" HeaderStyle-Width="100px" />
                <telerik:GridTemplateColumn UniqueName="Time" HeaderText="Problem List" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Problem")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Time" HeaderText="Care Plan" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Planning")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Time" HeaderText="Goal" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Goal")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Time" HeaderText="Target Time" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        <%# string.Format("{0} x {1} {2}", DataBinder.Eval(Container.DataItem, "IterationQty"), DataBinder.Eval(Container.DataItem, "Qty"), DataBinder.Eval(Container.DataItem, "SRTimeTypeName"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete"
                            Visible='<%#!(DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true)) %>'
                            OnClientClick="javascript: if (!confirm('Delete this record, are you sure ?')) return false;">
                        <img style="border: 0px; vertical-align: middle;" src="../../../../../Images/Toolbar/row_delete16.png" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEntry" runat="server">
    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%;vertical-align: top;">
                <table style="width: 100%;">
                    <tr>
                        <td class="label">No</td>
                        <td>
                            <telerik:RadTextBox ID="txtSeqNo" runat="server" Width="50px" Enabled="False" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Problem List</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtProblem" runat="server" TextMode="MultiLine" Width="100%" Height="80px" Resize="Vertical" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Care Plan</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPlanning" runat="server" TextMode="MultiLine" Width="100%" Height="80px" Resize="Vertical" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>

                </table>

            </td>
            <td style="vertical-align: top;">
                <table style="width: 100%">
                    <tr><td>&nbsp;</td><td></td><td></td><td></td></tr>
                    <tr>
                        <td class="label">Goal</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGoal" runat="server" TextMode="MultiLine" Width="100%" Height="80px" Resize="Vertical" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Description required."
                                ValidationGroup="Entry" ControlToValidate="txtGoal"
                                SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Time Target</td>
                        <td class="entry">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 40px">
                                        <telerik:RadNumericTextBox NumberFormat-DecimalDigits="0"
                                            ID="txtIterationQty" runat="server"
                                            Width="40px">
                                        </telerik:RadNumericTextBox></td>
                                    <td style="width: 15px; text-align: center">X</td>
                                    <td style="width: 40px">
                                        <telerik:RadNumericTextBox NumberFormat-DecimalDigits="0"
                                            ID="txtQty" runat="server"
                                            Width="40px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td style="width: 60px; padding-left: 4px;">
                                        <telerik:RadComboBox runat="server" ID="cboSRTimeType" Width="60px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td></td>

                                </tr>
                            </table>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>

            </td>
        </tr>

    </table>
</asp:Content>
