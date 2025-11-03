<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PrescriptionCopyDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.PrescriptionCopyDialog"
    Title="Prescription Copy" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ValidationSummary ID="vsumTransPrescriptionItem" runat="server" ValidationGroup="TransPrescriptionItem" />
    <asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="TransPrescriptionItem"
        ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>

    <asp:CheckBox runat="server" ID="chkIsUseIntervention" Checked="false" Visible="false" />
    <telerik:RadGrid ID="gridItem" runat="server" OnNeedDataSource="gridItem_NeedDataSource"
        OnItemCreated="gridItem_ItemCreated" OnDetailTableDataBind="gridItem_DetailTableDataBind"
        AutoGenerateColumns="False" GridLines="None" AllowPaging="False"
        AllowSorting="False">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView Name="master" CommandItemDisplay="None" DataKeyNames="HeaderNo,SequenceNo"
            ClientDataKeyNames="HeaderNo,SequenceNo"
            HierarchyLoadMode="ServerBind"
            HierarchyDefaultExpanded="true" ExpandCollapseColumn-Display="false">
            <Columns>
                <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="No" UniqueName="SequenceNo"
                    SortExpression="SequenceNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="55px" Visible="false" />
                <telerik:GridBoundColumn DataField="ParentNo" HeaderText="Parent No" UniqueName="ParentNo"
                    SortExpression="ParentNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="55px" Visible="false" />
                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                    SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="" UniqueName="R/"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="50px">
                    <ItemTemplate>
                        <asp:Label ID="lblR" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsRFlag")) ? "R/" : "" %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="ItemNameC"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  HeaderStyle-Width="250px">
                    <HeaderTemplate>
                        <div  <%# string.IsNullOrWhiteSpace(TemplateNo) ? string.Empty: "style=\"display:none\"" %> >
                            <asp:CheckBox runat="server" ID="chkIsUseIntervention2" Text="Use Drug Intervention" Checked="<%#chkIsUseIntervention.Checked%>" AutoPostBack="true" OnCheckedChanged="chkIsUseIntervention_CheckedChanged" />
                            </div>
                        <div  <%# !string.IsNullOrWhiteSpace(TemplateNo) ? string.Empty: "style=\"display:none\"" %> >
                        Item Name
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) ? "Compound" : DataBinder.Eval(Container.DataItem, "ItemName") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="Result Qty" UniqueName="ItemQtyInString" HeaderStyle-Width="80px"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <table style="padding: 0; margin: 0;">
                            <tr style="padding: 0; margin: 0;">
                                <td style="padding: 0; margin: 0;">
                                    <telerik:RadTextBox ID="txtItemQtyInString" runat="server" Width="40px" CssClass="RightAligned"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "ItemQtyInString") %>' />
                                </td>
                                <td style="padding: 0; margin: 0;">
                                    <asp:RequiredFieldValidator ID="rfvItemQtyInString" runat="server" ErrorMessage="Qty required."
                                        ValidationGroup="TransPrescriptionItem" ControlToValidate="txtItemQtyInString" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="EmbalaceLabel" HeaderText="Embalace Label" UniqueName="EmbalaceLabel"
                    SortExpression="EmbalaceLabel" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn DataField="SRItemUnit" HeaderText="Item Unit" UniqueName="SRItemUnit"
                    SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="TemplateSRItemUnit" HeaderStyle-Width="90px"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblSRItemUnit" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) ? DataBinder.Eval(Container.DataItem, "EmbalaceLabel") : DataBinder.Eval(Container.DataItem, "SRItemUnit") %>' />
                        <telerik:RadComboBox runat="server" ID="cboEmbalace" Width="79px" Visible="False" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SRConsumeMethod" HeaderText="SRConsumeMethod"
                    UniqueName="SRConsumeMethod" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="Consume Method" UniqueName="SRConsumeMethodCbo" HeaderStyle-Width="135px"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <telerik:RadComboBox ID="cboConsumeMethod" runat="server" Width="120px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Qty" UniqueName="ConsumeQty" HeaderStyle-Width="80px"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <table style="padding: 0; margin: 0;">
                            <tr style="padding: 0; margin: 0;">
                                <td style="padding: 0; margin: 0;">
                                    <telerik:RadTextBox ID="txtConsumeQty" runat="server" Width="40px" CssClass="RightAligned"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "ConsumeQty") %>' />
                                </td>
                                <td style="padding: 0; margin: 0;">
                                    <asp:RequiredFieldValidator ID="rfvConsumeQty" runat="server" ErrorMessage="Consume Qty required."
                                        ValidationGroup="TransPrescriptionItem" ControlToValidate="txtConsumeQty" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SRConsumeUnit" HeaderText="Unit"
                    UniqueName="SRConsumeUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="SRConsumeUnitCbo" HeaderStyle-Width="135px"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <telerik:RadComboBox ID="cboConsumeUnit" runat="server" Width="120px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SRMedicationConsume" HeaderText="SRMedicationConsume"
                                         UniqueName="SRMedicationConsume"  Visible="false" />

                <telerik:GridTemplateColumn HeaderText="Consume" UniqueName="SRMedicationConsumeCbo" HeaderStyle-Width="135px"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <telerik:RadComboBox ID="cboMedicationConsume" runat="server" Width="120px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Notes" UniqueName="Notes" HeaderStyle-Width="200px"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:TextBox ID="txtNotes" runat="server" Width="180px" Text='<%# (string)DataBinder.Eval(Container.DataItem, "Notes") %>'></asp:TextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="IsCompound" HeaderText="Is Compound" UniqueName="IsCompound"
                    SortExpression="IsCompound" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn DataField="EmbalaceID" HeaderText="Embalace ID" UniqueName="EmbalaceID"
                    SortExpression="EmbalaceID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn DataField="SRDosageUnit" HeaderText="Dosage Unit" UniqueName="SRDosageUnit"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Display="false" />
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="detailCompound" DataKeyNames="SequenceNo" AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="" UniqueName="Spacer" HeaderStyle-Width="20px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                &nbsp;
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="No" UniqueName="SequenceNo"
                            SortExpression="SequenceNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="55px"
                            Visible="false" />
                        <telerik:GridBoundColumn DataField="ParentNo" HeaderText="Parent No" UniqueName="ParentNo"
                            SortExpression="ParentNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="55px"
                            Visible="false" />
                        <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                            SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="250px"/>
                        <telerik:GridBoundColumn DataField="SRDosageUnit" HeaderText="Dosage Unit" UniqueName="SRDosageUnit"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Display="false" />
                        <telerik:GridTemplateColumn HeaderText="Formula" UniqueName="Formula" HeaderStyle-Width="210px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <table style="padding: 0; margin: 0;">
                                    <tr style="padding: 0; margin: 0;">
                                        <td style="padding: 0; margin: 0;">
                                            <telerik:RadTextBox ID="txtDosageQty" runat="server" Width="40px" CssClass="RightAligned"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "DosageQty") %>' />
                                        </td>
                                        <td style="padding: 0; margin: 0;">
                                            <telerik:RadComboBox ID="cboDosageUnit" runat="server" Width="120px" />
                                        </td>
                                        <td style="padding: 0; margin: 0;">
                                            <asp:RequiredFieldValidator ID="rfvDosageQty" runat="server" ErrorMessage="Formula Qty required."
                                                ValidationGroup="TransPrescriptionItem" ControlToValidate="txtDosageQty" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Notes" UniqueName="Notes" HeaderStyle-Width="200px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNotes" runat="server" Width="180px" Text='<%# (string)DataBinder.Eval(Container.DataItem, "Notes") %>'></asp:TextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="IsCompound" HeaderText="Is Compound" UniqueName="IsCompound"
                            SortExpression="IsCompound" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn DataField="EmbalaceID" HeaderText="Embalace ID" UniqueName="EmbalaceID"
                            SortExpression="EmbalaceID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn DataField="SRItemUnit" HeaderText="Item Unit" UniqueName="SRItemUnit"
                            SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>

    <table width="100%" style="position: fixed; bottom: 0; width: 100%; background-color: ButtonFace; height: 40px;">
        <tr>
            <td align="center">
                
                <asp:Button ID="btnAdd" runat="server" Text="Add to Prescription" Width="150" OnClick="btnAdd_Click" />&nbsp;
                        <asp:Button ID="btnOverwrite" runat="server" Text="Overwrite Prescription" Width="150" OnClick="btnOverwrite_Click" OnClientClick="return (confirm('Overwrite current prescription?'));" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70" OnClientClick="Close();return false;" />
            </td>
        </tr>
    </table>
</asp:Content>
