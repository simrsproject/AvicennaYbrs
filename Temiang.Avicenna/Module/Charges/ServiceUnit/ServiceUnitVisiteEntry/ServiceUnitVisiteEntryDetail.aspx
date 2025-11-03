<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ServiceUnitVisiteEntryDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ServiceUnitVisiteEntryDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server">

        <script type="text/javascript">
            function openWinCloseVisitPackage(pmno, patid, itid) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl("ServiceUnitVisiteClose.aspx.aspx?pmno=" + pmno + "&patid=" + patid + "&itid=" + itid);
                oWnd.set_title('Close Visit Package');
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" ID="winDialog" Animation="None" Behaviors="Move, Close"
        Width="400px" Height="300px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPaymentNo" runat="server" Text="Payment No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPaymentNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvPaymentNo" runat="server" ErrorMessage="Payment No required."
                                ValidationGroup="entry" ControlToValidate="txtPaymentNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPaymentDate" runat="server" Text="Start Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtPaymentDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPaymentTime" runat="server" Width="50px" MaxLength="5"
                                            ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Payment Type
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblPaymentType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Deposit Payment" Value="0" Selected="True" />
                                <asp:ListItem Text="Per Visit" Value="1" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="20"
                                ReadOnly="true" />
                            &nbsp;
                            <asp:Label runat="server" ID="lblServiceUnitName"></asp:Label>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvServiceUnit" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="txtServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                ValidationGroup="entry" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPrintReceiptAsName" runat="server" Text="Receipt As Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPrintReceiptAsName" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGuarantorID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label runat="server" ID="lblGuarantorName"></asp:Label>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdVisiteItem" runat="server" OnNeedDataSource="grdVisiteItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdVisiteItem_UpdateCommand"
        OnDeleteCommand="grdVisiteItem_DeleteCommand" OnInsertCommand="grdVisiteItem_InsertCommand"
        ShowFooter="true">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="PaymentNo, PatientID, ItemID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px" HeaderText="Close">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" onclick=\"openWinCloseVisitPackage('{0}','{1}','{2}'); return false;\">{3}</a>",
                                     DataBinder.Eval(Container.DataItem, "PaymentNo"), 
                                     DataBinder.Eval(Container.DataItem, "PatientID"),
                                     DataBinder.Eval(Container.DataItem, "ItemID"),
                                    "<img src=\"../../../../Images/Toolbar/close.png\" border=\"0\" alt=\"Close Visit Package\" title=\"Close Visit Package\" />"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                    SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-Width="100px" Visible="false" />
                <telerik:GridDateTimeColumn DataField="ExpiredDate" HeaderText="Expired Date" UniqueName="ExpiredDate"
                    SortExpression="ExpiredDate">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="VisiteQty" HeaderText="Visite Qty"
                    UniqueName="VisiteQty" SortExpression="VisiteQty" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" FooterAggregateFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Discount (%)"
                    UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" FooterAggregateFormatString="{0:n2}" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                    DataType="System.Double" DataFields="VisiteQty,Price,Discount" SortExpression="Total"
                    Expression="({0} * {1}) - ({2}/100 * ({0} * {1}))" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="ItemVisiteDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="ItemVisiteDetailEditCommand">
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
</asp:Content>
