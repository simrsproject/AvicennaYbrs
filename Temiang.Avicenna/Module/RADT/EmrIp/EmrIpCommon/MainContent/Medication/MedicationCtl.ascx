<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MedicationCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.MedicationCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<telerik:RadCodeBlock ID="radCodeBlock" runat="server">
</telerik:RadCodeBlock>

<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="grdMedicationReceive">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdMedicationReceive" />

            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>


<telerik:RadGrid ID="grdMedicationReceive" runat="server" OnNeedDataSource="grdMedicationReceive_NeedDataSource" OnDetailTableDataBind="grdMedicationReceive_DetailTableDataBind"
    AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdMedicationReceive_DeleteCommand" Height="560px"
    OnItemCommand="grdMedicationReceive_ItemCommand" OnItemDataBound="grdMedicationReceive_ItemDataBound">
    <MasterTableView DataKeyNames="MedicationReceiveNo,IsVoid">
        <Columns>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="250px"  HeaderText="Item">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ItemDescription")%><br/>
                    <%# DataBinder.Eval(Container.DataItem, "SRConsumeMethodName")%>&nbsp;@<%# DataBinder.Eval(Container.DataItem, "ConsumeQty")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "SRItemUnit")%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="200px"  HeaderText="This Day Medication">
                <ItemTemplate>
                    <%# MedicationRealizationHtml(DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"), DataBinder.Eval(Container.DataItem, "SRConsumeMethod"), DataBinder.Eval(Container.DataItem, "IsVoid"), DataBinder.Eval(Container.DataItem, "BalanceQty"), DataBinder.Eval(Container.DataItem, "ConsumeQty"),0,DataBinder.Eval(Container.DataItem, "IsContinue"))%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="160px"  HeaderText="This Day + 1">
                <ItemTemplate>
                    <%# MedicationRealizationHtml(DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"), DataBinder.Eval(Container.DataItem, "SRConsumeMethod"), DataBinder.Eval(Container.DataItem, "IsVoid"), DataBinder.Eval(Container.DataItem, "BalanceQty"), DataBinder.Eval(Container.DataItem, "ConsumeQty"),1,DataBinder.Eval(Container.DataItem, "IsContinue"))%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="160px"  HeaderText="This Day + 2">
                <ItemTemplate>
                    <%# MedicationRealizationHtml(DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"), DataBinder.Eval(Container.DataItem, "SRConsumeMethod"), DataBinder.Eval(Container.DataItem, "IsVoid"), DataBinder.Eval(Container.DataItem, "BalanceQty"), DataBinder.Eval(Container.DataItem, "ConsumeQty"),2,DataBinder.Eval(Container.DataItem, "IsContinue"))%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridDateTimeColumn DataField="ReceiveDateTime" UniqueName="ReceiveDateTime" HeaderText="Receive Time"
                HeaderStyle-Width="110px" />
            <telerik:GridNumericColumn DataField="ReceiveQty" UniqueName="ReceiveQty" HeaderText="Rec. Qty" HeaderStyle-Width="60px" />
            <telerik:GridNumericColumn DataField="BalanceQty" UniqueName="BalanceQty" HeaderText="Bal. Qty" HeaderStyle-Width="60px" />
            <telerik:GridBoundColumn DataField="SRItemUnit" UniqueName="SRItemUnit" HeaderText="Unit" HeaderStyle-Width="60px" />
            <telerik:GridBoundColumn DataField="PrescriptionNo" UniqueName="PrescriptionNo" HeaderText="Prescription No" HeaderStyle-Width="100px" />
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px" Visible="False">
                <ItemTemplate>
                    <%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ReceiveDateTime")).Date < DateTime.Now.Date || (DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true)) ? string.Format("<img src=\"{0}/Images/Toolbar/edit16_d.png\" />",Helper.UrlRoot()) : string.Format("<a href=\"#\" onclick=\"javascript:entryMedicationReceive('edit', '{0}'); return false;\"><img src=\"{1}/Images/Toolbar/edit16.png\"  alt=\"New\" /></a>",DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"),Helper.UrlRoot()))%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                <ItemTemplate>
                    <%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ReceiveDateTime")).Date < DateTime.Now.Date || (DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))  ? string.Format("<img src=\"{0}/Images/Toolbar/row_delete16_d.png\" />",Helper.UrlRoot()) : "")%>
                    <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete"
                        Visible='<%#!(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ReceiveDateTime")) < DateTime.Now.Date || (DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))) %>'
                        OnClientClick="javascript: if (!confirm('Void this Medication, are you sure ?')) return false;">
                        <img style="border: 0px; vertical-align: middle;" src="<%= Helper.UrlRoot() %>/Images/Toolbar/row_delete16.png" />
                    </asp:LinkButton>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
        </Columns>
        <DetailTables>
            <telerik:GridTableView DataKeyNames="MedicationReceiveNo, SequenceNo" Name="grdMedicationReceiveUsed" Width="100%"
                AutoGenerateColumns="false" ShowFooter="true" AllowPaging="true" PageSize="10">
                <Columns>
                    <telerik:GridDateTimeColumn DataField="RealizedDateTime" HeaderText="Medication Hist." UniqueName="RealizedDateTime" HeaderStyle-Width="120px" />
                    <telerik:GridDateTimeColumn DataField="ScheduleDateTime" HeaderText="Schedule" UniqueName="ScheduleDateTime" HeaderStyle-Width="120px" />
                    <telerik:GridBoundColumn DataField="Qty" UniqueName="Qty" HeaderText="Qty" HeaderStyle-Width="60px" />
                    <telerik:GridBoundColumn DataField="Note" UniqueName="Note" HeaderText="Note" />
                    <telerik:GridTemplateColumn />
                </Columns>
            </telerik:GridTableView>
        </DetailTables>
    </MasterTableView>
    <ClientSettings EnableRowHoverStyle="False">
        <Selecting AllowRowSelect="False" />
        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
    </ClientSettings>
</telerik:RadGrid>