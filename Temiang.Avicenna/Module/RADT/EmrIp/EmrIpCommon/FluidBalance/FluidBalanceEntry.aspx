<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="FluidBalanceEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.FluidBalanceEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hdnEditRegistrationNo" />
    <asp:HiddenField runat="server" ID="hdnEditSequenceNo" />

    <table width="100%">
        <tr>
            <td class="label">Date / Seq No
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 100px">
                            <telerik:RadDatePicker ID="txtInOutDate" runat="server" Width="100px" Enabled="False" />
                        </td>
                        <td style="width: 93px">
                            <telerik:RadNumericTextBox ID="txtSequenceNo" runat="server" Width="90px" Enabled="False" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="20px"></td>
            <td></td>

        </tr>
        <tr runat="server" id="trSchemaInfusOldVersion">
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Schema Infus"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSchemaInfus" runat="server" Width="304px" MaxLength="250" />
            </td>
            <td width="20px"></td>
            <td></td>

        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Calculate IWL For (Hour)"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtIwlForHour" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
            </td>
            <td width="20px"></td>
            <td></td>
        </tr>
    </table>
    <telerik:RadAjaxPanel runat="server" ID="ajxPanel">
        <table width="100%">
            <tr>
                <td class="label">Body Weight
                </td>
                <td class="entry">
                    <telerik:RadNumericTextBox ID="txtBodyWeight" runat="server" Width="100px" NumberFormat-DecimalDigits="2" />
                    <asp:LinkButton runat="server" ID="lbtnResetBodyWeight" OnClick="lbtnResetBodyWeight_OnClick" OnClientClick="if (!confirm('Reset this Body Weight')) return false;">
                        <img src="../../../../../Images/Toolbar/refresh16.png"/>
                    </asp:LinkButton></td>
                <td width="20px"></td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Normal Temperature (C)
                    
                </td>
                <td class="entry">
                    <telerik:RadNumericTextBox ID="txtNormalTemp" runat="server" Width="100px" NumberFormat-DecimalDigits="2" />
                    <asp:LinkButton runat="server" ID="lbtnResetNormalTemp" OnClick="lbtnResetNormalTemp_OnClick" OnClientClick="if (!confirm('Reset this Last Temperature')) return false;">
                        <img src="../../../../../Images/Toolbar/refresh16.png"/>
                    </asp:LinkButton></td>
                <td width="20px"></td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Last Temperature (C)
                </td>
                <td class="entry">
                    <telerik:RadNumericTextBox ID="txtLastTemp" runat="server" Width="100px" NumberFormat-DecimalDigits="2" />
                    <asp:LinkButton runat="server" ID="lbtnResetLastTemp" OnClick="lbtnResetLastTemp_OnClick" OnClientClick="if (!confirm('Reset this Last Temperature')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                    </asp:LinkButton></td>
                <td width="20px"></td>

                <td></td>
            </tr>
            <tr>
                <td class="label">Constant IWL
                </td>
                <td class="entry">
                    <telerik:RadNumericTextBox ID="txtIwlConstant" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                </td>
                <td></td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>


    <telerik:RadGrid ID="grdSchemaInfus" Width="98%" runat="server" OnNeedDataSource="grdSchemaInfus_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdSchemaInfus_UpdateCommand"
        OnDeleteCommand="grdSchemaInfus_DeleteCommand" OnInsertCommand="grdSchemaInfus_InsertCommand"
        AllowMultiRowEdit="false">
        <MasterTableView DataKeyNames="SchemaInfusNo" CommandItemDisplay="Top">
            <CommandItemTemplate>
                <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdSchemaInfus.MasterTableView.IsItemInserted %>'>
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/insert16.png" />
                    &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new Schema Infus"></asp:Label>
                </asp:LinkButton>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" Visible="true">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="SchemaInfusName" HeaderText="Schema Infus" UniqueName="SchemaInfusName"
                    HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn DataField="QtyVolume" HeaderText="Volume" UniqueName="Volume"
                    HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" DecimalDigits="0" />
                <telerik:GridNumericColumn DataField="QtyPerHour" HeaderText="CC / Hour" UniqueName="QtyPerHour"
                    HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" DecimalDigits="0" />

                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings UserControlName="FluidBalanceSchemaInfusDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="SchemaInfusEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="false" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>

</asp:Content>
