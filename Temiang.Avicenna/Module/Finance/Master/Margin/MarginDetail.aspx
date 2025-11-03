<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="MarginDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.MarginDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblMarginID" runat="server" Text="Margin ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMarginID" runat="server" Width="100px" MaxLength="10" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvMarginID" runat="server" ErrorMessage="Margin ID required."
                    ValidationGroup="entry" ControlToValidate="txtMarginID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblMarginName" runat="server" Text="Margin Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMarginName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvMarginName" runat="server" ErrorMessage="Margin Name required."
                    ValidationGroup="entry" ControlToValidate="txtMarginName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
            </td>
            <td width="20"></td>
            <td></td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItemProductMarginValue" runat="server" OnNeedDataSource="grdItemProductMarginValue_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemProductMarginValue_UpdateCommand"
        OnDeleteCommand="grdItemProductMarginValue_DeleteCommand" OnInsertCommand="grdItemProductMarginValue_InsertCommand"
        OnDetailTableDataBind="grdItemProductMarginValue_DetailTableDataBind">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="MarginID, SequenceNo">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="35px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SequenceNo" HeaderText="No"
                    UniqueName="SequenceNo" SortExpression="SequenceNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="StartingValue" HeaderText="Starting Value"
                    UniqueName="StartingValue" SortExpression="StartingValue" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="EndingValue" HeaderText="Ending Value"
                    UniqueName="EndingValue" SortExpression="EndingValue" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MarginPercentage"
                    HeaderText="Global (%)" UniqueName="MarginPercentage" SortExpression="MarginPercentage"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsGlobalWithoutVAT" HeaderText="-VAT"
                    UniqueName="IsGlobalWithoutVAT" SortExpression="IsGlobalWithoutVAT" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="InpatientMarginPercentage"
                    HeaderText="Inpatient (%)" UniqueName="InpatientMarginPercentage" SortExpression="InpatientMarginPercentage"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsIpWithoutVAT" HeaderText="-VAT"
                    UniqueName="IsIpWithoutVAT" SortExpression="IsIpWithoutVAT" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="OutpatientMarginPercentage"
                    HeaderText="Outpatient (%)" UniqueName="OutpatientMarginPercentage" SortExpression="OutpatientMarginPercentage"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsOpWithoutVAT" HeaderText="-VAT"
                    UniqueName="IsOpWithoutVAT" SortExpression="IsOpWithoutVAT" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmergencyMarginPercentage"
                    HeaderText="Emergency (%)" UniqueName="EmergencyMarginPercentage" SortExpression="EmergencyMarginPercentage"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsEmWithoutVAT" HeaderText="-VAT"
                    UniqueName="IsEmWithoutVAT" SortExpression="IsEmWithoutVAT" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="OTCMarginPercentage"
                    HeaderText="OTC (%)" UniqueName="OTCMarginPercentage" SortExpression="OTCMarginPercentage"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsOtcWithoutVAT" HeaderText="-VAT"
                    UniqueName="IsOtcWithoutVAT" SortExpression="IsOtcWithoutVAT" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn DataField="IsMinusDiscount" HeaderText="Minus Discount"
                    UniqueName="IsMinusDiscount" SortExpression="IsMinusDiscount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridTemplateColumn />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="MarginID, SequenceNo, ClassID" Name="grdReferenceItem"
                    Width="100%" AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ClassName" HeaderText="Class Name" UniqueName="ClassName"
                            SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MarginValuePercentage"
                            HeaderText="Value (%)" UniqueName="MarginValuePercentage" SortExpression="MarginValuePercentage"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
            <EditFormSettings UserControlName="ItemProductMarginValueDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="ItemProductMarginDetailCommand">
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
