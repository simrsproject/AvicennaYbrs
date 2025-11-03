<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="DrugObservationEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PharmaceuticalCare.DrugObservationEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
        var _height = 0;
        var _width = 0;
        function onWinPickerClose(sender, args) {

            // Restore window size
            var curWnd = GetRadWindow();
            if (_width > 0) {
                curWnd.setSize(_width, _height);
                curWnd.center();
            }

            var arg = args.get_argument();
            if (arg != null) {

                if (arg.callbackMethod == "submit")
                    __doPostBack(arg.eventTarget, arg.eventArgument);
                else if (arg.callbackMethod == "rebind") {
                    var ctl = $find(arg.eventTarget);
                    if (typeof ctl.rebind == 'function') {
                        ctl.rebind();
                    } else {
                        var masterTable = $find(arg.eventTarget).get_masterTableView();
                        masterTable.rebind();
                    }
                }
            }
        }

        function openDrugItemPicker() {

            // Resize window
            var curWnd = GetRadWindow();
            var curWndBounds = curWnd.getWindowBounds();
            // Not Maximized
            if (curWndBounds.y > 0 & curWndBounds.x > 0) {
                _height = curWndBounds.height;
                _width = curWndBounds.width;

                var setHeight = 700;
                if (setHeight < _height)
                    setHeight = _height;

                var setWidth = 1200;
                if (setWidth < _width)
                    setWidth = _width;

                curWnd.setSize(setWidth, setHeight);
                curWnd.center();
            }

            var oWnd = $find("<%= winPicker.ClientID %>");
            oWnd.setUrl("DrugItemPicker.aspx?regno=<%=RegistrationNo%>&obsno=<%=txtDrugObsNo.Text%>&ccm=rebind&cet=<%=grdItem.ClientID %>");

            oWnd.setSize(1000, 600);
            oWnd.center();
            oWnd.show();
        }

    </script>
    <telerik:RadWindow ID="winPicker" Width="680px" Height="620px" runat="server" Modal="true"
        ShowContentDuringLoad="false" Behaviors="None" VisibleStatusbar="False"
        OnClientClose="onWinPickerClose">
    </telerik:RadWindow>
    <table width="500px">
        <tr>
            <td class="label">No 
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDrugObsNo" runat="server" Width="50px" Enabled="false" />
            </td>
            <td style="width:6px;">&nbsp;</td>
            <td class="label">Date 
            </td>
            <td class="entry">
                <telerik:RadDateTimePicker ID="txtDrugObsDateTime" runat="server" Width="170px" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Observation Time required."
                    ValidationGroup="entry" ControlToValidate="txtDrugObsDateTime" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 50%"><h2>.: Drug Related Problems</h2></td>
            <td>&nbsp;&nbsp;</td>
            <td style="width: 50%"><h2>.: PTO Screening (if ≥ 2 criteria are met)</h2></td>
        </tr>
        <tr>
            <td style="width: 50%" valign="top">
                <telerik:RadGrid ID="grdDrps" Width="99%" runat="server" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
                    AllowMultiRowSelection="True"
                    OnNeedDataSource="grdDrps_NeedDataSource" OnItemDataBound="grdDrps_ItemDataBound">
                    <MasterTableView DataKeyNames="ItemID" ShowHeader="true" ShowHeadersWhenNoRecords="false" Width="100%">
                        <Columns>
                            <telerik:GridBoundColumn DataField="ItemID" HeaderText="No" UniqueName="ItemID" HeaderStyle-Width="40px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ItemName" HeaderText="Criteria" UniqueName="ItemName" HeaderStyle-Width="250px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IsYes" UniqueName="IsYes" HeaderText="IsYes" Display="false" />
                            <telerik:GridTemplateColumn HeaderText="" UniqueName="optYesNo" HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <telerik:RadRadioButtonList runat="server" ID="optYesNo" AutoPostBack="false" Direction="Horizontal">
                                        <Items>
                                            <telerik:ButtonListItem Text="Ya" Value="1" />
                                            <telerik:ButtonListItem Text="Tidak" Value="0" />
                                        </Items>
                                    </telerik:RadRadioButtonList>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
                        <Resizing AllowColumnResize="False" />
                        <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td>&nbsp;&nbsp;</td>
            <td style="width: 50%" valign="top">
                <telerik:RadGrid ID="grdPto" Width="99%" runat="server" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
                    AllowMultiRowSelection="True"
                    OnNeedDataSource="grdPto_NeedDataSource" OnItemDataBound="grdPto_ItemDataBound">
                    <MasterTableView DataKeyNames="ItemID" ShowHeader="true" ShowHeadersWhenNoRecords="false" Width="100%">
                        <Columns>
                            <telerik:GridBoundColumn DataField="ItemID" HeaderText="No" UniqueName="ItemID" HeaderStyle-Width="40px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ItemName" HeaderText="Criteria" UniqueName="ItemName" HeaderStyle-Width="250px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IsYes" UniqueName="IsYes" HeaderText="IsYes" Display="false" />
                            <telerik:GridBoundColumn DataField="YesNotes" UniqueName="YesNotes" HeaderText="YesNotes" Display="false" />
                            <telerik:GridBoundColumn DataField="IsDrugDuplicate" UniqueName="IsDrugDuplicate" HeaderText="IsDrugDuplicate" Display="false" />
                            <telerik:GridBoundColumn DataField="IsMoreThan7Days" UniqueName="IsMoreThan7Days" HeaderText="IsMoreThan7Days" Display="false" />
                            <telerik:GridBoundColumn DataField="IsAgeMoreThan65y" UniqueName="IsAgeMoreThan65y" HeaderText="IsAgeMoreThan65y" Display="false" />
                            <telerik:GridBoundColumn DataField="IsSindromGeriatry" UniqueName="IsSindromGeriatry" HeaderText="IsSindromGeriatry" Display="false" />
                            <telerik:GridBoundColumn DataField="ReferenceID" UniqueName="ReferenceID" HeaderText="ReferenceID" Display="false" />
                            <telerik:GridTemplateColumn HeaderText="" UniqueName="optYesNo" HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <telerik:RadRadioButtonList runat="server" ID="optYesNo" AutoPostBack="false" Direction="Horizontal">
                                        <Items>
                                            <telerik:ButtonListItem Text="Ya" Value="1" />
                                            <telerik:ButtonListItem Text="Tidak" Value="0" />
                                        </Items>
                                    </telerik:RadRadioButtonList>
                                    <telerik:RadTextBox ID="txtYesNotes" runat="server" Width="150px" ReadOnly="<%# DataModeCurrent == AppEnum.DataMode.Read%>"></telerik:RadTextBox>
                                    <telerik:RadCheckBoxList runat="server" ID="chkList" AutoPostBack="false">
                                    </telerik:RadCheckBoxList>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
                        <Resizing AllowColumnResize="False" />
                        <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
                    </ClientSettings>
                </telerik:RadGrid>
                <table width="500px">
                    <tr>
                        <td class="label">Need PTO
                        </td>
                        <td class="entry">
                            <telerik:RadRadioButtonList runat="server" ID="optIsNeedPto" AutoPostBack="false" Direction="Horizontal">
                                <Items>
                                    <telerik:ButtonListItem Text="Yes" Value="1" />
                                    <telerik:ButtonListItem Text="No" Value="0" />
                                </Items>
                            </telerik:RadRadioButtonList>
                        </td>
                        <td></td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
    <h2>.: Drug List</h2>
    <telerik:RadGrid ID="grdItem" Width="99%" runat="server" RenderMode="Lightweight"
        AutoGenerateColumns="False" EnableViewState="true"
        AllowMultiRowSelection="True"
        OnNeedDataSource="grdItem_NeedDataSource" OnDeleteCommand="grdItem_DeleteCommand">
        <MasterTableView DataKeyNames="MedicationReceiveNo" ShowHeader="true" ShowHeadersWhenNoRecords="true" Width="100%" CommandItemDisplay="Top">
            <CommandItemTemplate>
                <asp:LinkButton ID="lbOpenPicker" runat="server" Visible='<%# (DataModeCurrent != AppEnum.DataMode.Read) %>' OnClientClick="openDrugItemPicker();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/insert16.png" />
                    &nbsp;Add Drug Item
                </asp:LinkButton>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridBoundColumn DataField="ItemDescription" HeaderText="Item Description" UniqueName="ItemDescription" HeaderStyle-Width="250px"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ConsumeMethod" HeaderText="Consume Method" UniqueName="ConsumeMethod" HeaderStyle-Width="200px">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Follow-up" UniqueName="FollowUp" HeaderStyle-Width="250px" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadTextBox
                            ID="txtFollowUp" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "FollowUp")%>' ReadOnly="<%# DataModeCurrent == AppEnum.DataMode.Read%>"
                            Width="100%">
                        </telerik:RadTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Notes" UniqueName="Notes" HeaderStyle-Width="250px" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadTextBox
                            ID="txtNotes" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "Notes")%>' ReadOnly="<%# DataModeCurrent == AppEnum.DataMode.Read%>"
                            Width="100%">
                        </telerik:RadTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete" Visible='<%# (DataModeCurrent != AppEnum.DataMode.Read) %>'
                            OnClientClick="javascript: if (!confirm('Delete this drug item ?')) return false;">
                        <img style="border: 0px; vertical-align: middle;" src="<%#Helper.UrlRoot()%>/Images/Toolbar/row_delete16.png" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
            <Resizing AllowColumnResize="False" />
            <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
        </ClientSettings>
    </telerik:RadGrid>

    <br />

    <table width="100%">
        <tr>
            <td style="width: 50%"><b>.: Drug Interaction Risk/Related Problems</b></td>
            <td>&nbsp;&nbsp;</td>
            <td style="width: 50%"><b>.: Recommendation</b></td>
        </tr>
        <tr>
            <td style="width: 50%">
                <telerik:RadTextBox ID="txtDrugInteractionRisk" TextMode="MultiLine" runat="server" Width="100%" Height="100px" MaxLength="300" /></td>
            <td>&nbsp;&nbsp;</td>
            <td style="width: 50%">
                <telerik:RadTextBox ID="txtRecommendation" TextMode="MultiLine" runat="server" Width="100%" Height="100px" MaxLength="300" /></td>
        </tr>
    </table>

</asp:Content>
