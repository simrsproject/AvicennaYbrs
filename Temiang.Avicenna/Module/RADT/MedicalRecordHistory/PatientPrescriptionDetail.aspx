<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PatientPrescriptionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientPrescriptionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchRegistrationNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" />
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td style="width: 50%">
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                    OnDetailTableDataBind="grdList_DetailTableDataBind" AutoGenerateColumns="false"
                    OnItemCommand="grdList_ItemCommand">
                    <MasterTableView DataKeyNames="PrescriptionNo">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="RegistrationNo" HeaderText="Registration No ">
                                    </telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="RegDateRegNo" SortOrder="Descending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="RegistrationNo" HeaderText="Registration No"
                                UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="False" />
                            <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="PrescriptionNo" HeaderText="Prescription No"
                                UniqueName="PrescriptionNo" SortExpression="PrescriptionNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridDateTimeColumn HeaderStyle-Width="60px" DataField="PrescriptionDate"
                                HeaderText="Date" UniqueName="PrescriptionDate" SortExpression="PrescriptionDate"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Dispensary Name"
                                UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsFromSOAP" HeaderText="Order"
                                UniqueName="IsFromSOAP" SortExpression="IsFromSOAP" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsPrescriptionReturn"
                                HeaderText="Return" UniqueName="IsPrescriptionReturn" SortExpression="IsPrescriptionReturn"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridTemplateColumn UniqueName="Print" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnPrint" runat="server" CommandName="Print" ToolTip='Print Prescription'
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PrescriptionNo") %>'>
                                            <img src="../../../Images/Toolbar/print16.png" border="0" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Print2" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnPrint2" runat="server" CommandName="Print2" ToolTip='Print Prescription Receipt'
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PrescriptionNo") %>'>
                                            <img src="../../../Images/Toolbar/print16.png" border="0" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <DetailTables>
                            <telerik:GridTableView DataKeyNames="SequenceNo" Name="grdDetail" AutoGenerateColumns="False">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="Sequence No" UniqueName="SequenceNo"
                                        HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="30px" DataField="IsRFlag" HeaderText="R/"
                                        UniqueName="IsRFlag" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' /><br />
                                            <i>Intervention :</i><asp:Label ID="lblIntervention" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemInterventionName") %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="ResultQty" HeaderText="Qty"
                                        UniqueName="ResultQty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                                        UniqueName="SRItemUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="FrequencyOfDosing"
                                        HeaderText="Frequency" UniqueName="FrequencyOfDosing" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="DosingPeriod" HeaderText="Times"
                                        UniqueName="DosingPeriod" SortExpression="DosingPeriod" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="DiscountAmount" HeaderText="Discount"
                                        UniqueName="DiscountAmount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                        DataFormatString="{0:n2}" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Total" HeaderText="Total"
                                        UniqueName="Total" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                        DataFormatString="{0:n2}" />
                                </Columns>
                            </telerik:GridTableView>
                        </DetailTables>
                    </MasterTableView>
                    <ClientSettings AllowDragToGroup="true" EnableRowHoverStyle="true" AllowExpandCollapse="true">
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
</asp:Content>
