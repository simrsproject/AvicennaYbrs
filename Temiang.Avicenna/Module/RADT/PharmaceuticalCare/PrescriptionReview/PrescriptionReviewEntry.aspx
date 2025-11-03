<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="PrescriptionReviewEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PharmaceuticalCare.PrescriptionReviewEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/RegistrationInfoCtl.ascx" TagPrefix="uc1" TagName="RegistrationInfoCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var _activeRowIndex = "0";

        function rightStatusAll(type) {
            if (!confirm('Set all to ' + type + ' ?')) return false;

            var grid = $find("<%= grdEntry.ClientID %>");
            if (grid) {
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                var status = type == "Yes" ? 1 : type == "No" ? 2 : 0;
                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];
                    var optRight = $telerik.findControl(row.get_element(), "optRight")
                    optRight.set_selectedIndex(status);
                }

            }
        }
        function RightStatusSelectedIndexChanging(sender, args) {
            //var selectedLanguage = args.get_item().get_text();
            //var toChange = !confirm("You are about to change to " + selectedLanguage + " language!");
            //args.set_cancel(toChange);
        }
        function RightStatusSelectedIndexChanged(sender, args) {
            //var oldItem = sender.get_items()[args.get_oldSelectedIndex()];
            //var newItem = sender.get_items()[args.get_newSelectedIndex()];

            if (args.get_newSelectedIndex() == 2) {
                _activeRowIndex = (parseInt(sender.get_element().getAttribute("RowIndex")) / 2) - 1; //ItemIndex yg didapat pada ItemDataBound aneh

                if (_activeRowIndex < 4) return;

                var grid = $find("<%= grdEntry.ClientID %>");
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                var itemName = rows[_activeRowIndex].get_cell("ItemName").innerHTML;

                var oWnd = $find("<%= winPicker.ClientID %>");
                oWnd.setUrl("PrescriptionReviewItemPicker.aspx?patid=<%=PatientID %>&regno=<%=RegistrationNo %>&pn=<%=PrescriptionNo %>&rv=" + escape(itemName));

                oWnd.setSize(1000, 600);
                oWnd.center();
                oWnd.show();
            }
        }


        function changeConfirm(sender, args) {

        }

        function onPickerClose(sender, eventArgs) {

            // Get retval
            var arg = eventArgs.get_argument();
            if (arg) {
                var dataItems = $find('<%=grdEntry.ClientID%>').get_masterTableView().get_dataItems();
                var tgt = $telerik.findControl(dataItems[_activeRowIndex].get_element(), "txtInformation")
                var prevVal = tgt.get_value();
                if (prevVal != "")
                    tgt.set_value(prevVal + '\r' + unescape(arg.retval));
                else
                    tgt.set_value(unescape(arg.retval));
            }
        }
    </script>

    <telerik:RadWindow ID="winPicker" Width="680px" Height="620px" runat="server" Modal="true"
        ShowContentDuringLoad="false" Behaviors="None" VisibleStatusbar="False"
        OnClientClose="onPickerClose">
    </telerik:RadWindow>
    <uc1:RegistrationInfoCtl runat="server" ID="RegistrationInfoCtl" />
    <fieldset>
        <legend><strong>Prescription Item</strong></legend>
        <telerik:RadGrid ID="grdPrescriptionItem" runat="server" OnNeedDataSource="grdPrescriptionItem_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None" AllowPaging="false">
            <MasterTableView DataKeyNames="SequenceNo">
                <Columns>
                    <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="Seq. No" UniqueName="SequenceNo"
                        HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsCompound" HeaderText="C"
                        UniqueName="IsCompound" SortExpression="IsCompound" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="ParentNo" HeaderText="Header" UniqueName="ParentNo"
                        HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderStyle-Width="35px"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblR" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsRFlag")) ? @"R/" : "" %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                        <ItemTemplate>
                            <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Numero" UniqueName="TemplateItemName3" HeaderStyle-Width="60px"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "PrescriptionQty") %><br />
                            (<%# DataBinder.Eval(Container.DataItem, "ResultQty") %>)
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="TemplateItemName4" HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) ? DataBinder.Eval(Container.DataItem, "EmbalaceLabel") : DataBinder.Eval(Container.DataItem, "SRItemUnit")%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="DosageQty" HeaderText="Dosing"
                        UniqueName="DosageQty" SortExpression="DosageQty" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" />
                    <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRDosageUnit" HeaderText="Unit"
                        UniqueName="SRDosageUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn DataField="SRConsumeMethodName" HeaderText="Consume Method"
                        UniqueName="SRConsumeMethodName" SortExpression="SRConsumeMethodName" HeaderStyle-Width="100px" />
                </Columns>


            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="False">
                <Selecting AllowRowSelect="False" />
            </ClientSettings>
        </telerik:RadGrid>
    </fieldset>

    <fieldset>
        <legend><strong>Prescription Review</strong></legend>
        <telerik:RadGrid ID="grdEntry" runat="server" OnNeedDataSource="grdEntry_NeedDataSource" OnItemDataBound="grdEntry_ItemDataBound" OnItemCommand="grdEntry_ItemCommand"
            AutoGenerateColumns="False" GridLines="None" AllowPaging="false">
            <MasterTableView DataKeyNames="ItemID">
                <Columns>
                    <telerik:GridBoundColumn DataField="RightStatus" UniqueName="RightStatus" Display="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ItemName" HeaderStyle-Width="250px" HeaderText="Review"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="" UniqueName="RightStatus" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <center>
                                Right Status
                                <div style="display: <%# DisplayMenuRightStatusAll() %>">
                                    <table>
                                        <tr>
                                            <td>
                                                <input type="button" value="Not Review" onclick="rightStatusAll('Unselect')" /></td>
                                            <td>
                                                <input type="button" value="Yes" onclick="rightStatusAll('Yes')" /></td>
                                        </tr>
                                    </table>

                                </div>
                            </center>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <center>
                                <telerik:RadRadioButtonList
                                    ID="optRight" runat="server" Direction="Horizontal" Visible="<%# DataModeCurrent != AppEnum.DataMode.Read%>"
                                    Width="100%" AutoPostBack="false">
                                    <ClientEvents OnSelectedIndexChanging="RightStatusSelectedIndexChanging" OnSelectedIndexChanged="RightStatusSelectedIndexChanged" />
                                    <Items>
                                        <telerik:ButtonListItem Text="Not Review" Value="" />
                                        <telerik:ButtonListItem Text="Yes" Value="1" />
                                        <telerik:ButtonListItem Text="No" Value="0" />
                                    </Items>
                                </telerik:RadRadioButtonList>
                                <%# RightStatusInfo(Container)%>
                            </center>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Information" UniqueName="Information" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadTextBox
                                ID="txtInformation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Information")%>' ReadOnly="<%# DataModeCurrent == AppEnum.DataMode.Read%>"
                                Width="100%" TextMode="MultiLine" Resize="Vertical">
                            </telerik:RadTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="False">
                <Selecting AllowRowSelect="False" />
            </ClientSettings>
        </telerik:RadGrid>
    </fieldset>
</asp:Content>
