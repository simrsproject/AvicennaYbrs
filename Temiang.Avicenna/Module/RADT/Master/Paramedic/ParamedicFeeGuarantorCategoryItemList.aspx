<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ParamedicFeeGuarantorCategoryItemList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ParamedicFeeGuarantorCategoryItemList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
            function openGuarantorCategoryItemComp(itemId) {
                var oPar = $find("<%= txtParamedicID.ClientID %>");
                var oCat = $find("<%= cboSRPhysicianFeeType.ClientID %>");
                var oWnd = $find("<%= winItemGuarantor.ClientID %>");
                oWnd.SetUrl("ParamedicFeeGuarantorCategoryItemCompList.aspx?paramedicID=" + oPar.get_value() + "&feeType=" + oCat.get_value() + "&itemId=" + itemId);
                oWnd.Show();
                oWnd.Maximize();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winItemGuarantor" Animation="None" Width="800px" Height="500px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy runat="server" ID="RadAjaxManagerProxy1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicID" runat="server" Width="100px" MaxLength="10"
                                ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblParamedicName" runat="server"></asp:Label>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRPhysicianFeeType" runat="server" Text="Guarantor Category"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRPhysicianFeeType" runat="server" Width="300px" Enabled="False">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <telerik:RadGrid ID="grdDetail" runat="server" OnNeedDataSource="grdDetail_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdDetail_UpdateCommand"
                    OnDeleteCommand="grdDetail_DeleteCommand" OnInsertCommand="grdDetail_InsertCommand">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="ParamedicID,SRPhysicianFeeType,ItemID">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="30px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                                SortExpression="ItemID">
                                <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                SortExpression="ItemName">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="IsParamedicFeeUsePercentage" HeaderText="Fee Using Percentage"
                                UniqueName="IsParamedicFeeUsePercentage" SortExpression="IsParamedicFeeUsePercentage">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridBoundColumn DataField="ParamedicFeeAmount" HeaderText="Fee Amount" UniqueName="ParamedicFeeAmount"
                                SortExpression="ParamedicFeeAmount" DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ParamedicFeeAmountReferral" HeaderText="Fee Amount Referral"
                                UniqueName="ParamedicFeeAmountReferral" SortExpression="ParamedicFeeAmountReferral"
                                DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="IsDeductionFeeUsePercentage" HeaderText="Deduction Using Percentage"
                                UniqueName="IsDeductionFeeUsePercentage" SortExpression="IsDeductionFeeUsePercentage">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridBoundColumn DataField="DeductionFeeAmount" HeaderText="Deduction Amount"
                                UniqueName="DeductionFeeAmount" SortExpression="DeductionFeeAmount" DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DeductionFeeAmountReferral" HeaderText="Deduction Amount Referral"
                                UniqueName="DeductionFeeAmountReferral" SortExpression="DeductionFeeAmountReferral"
                                DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                            <telerik:GridTemplateColumn UniqueName="processComp">
                                <ItemTemplate>
                                    <%# string.Format("<a href=\"#\" onclick=\"openGuarantorCategoryItemComp('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"Setting Guarantor Category - Item - Component\" /></a>",
                                                                                                            DataBinder.Eval(Container.DataItem, "ItemID"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="35px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <EditFormSettings UserControlName="ParamedicFeeGuarantorCategoryItemListDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="grdParamedicFeeGuarantorCategoryItemEditCommand">
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
</asp:Content>