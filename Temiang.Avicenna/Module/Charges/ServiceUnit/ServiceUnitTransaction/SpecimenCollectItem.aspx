<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" 
    CodeBehind="SpecimenCollectItem.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.SpecimenCollectItem" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>            
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>    
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" 
        OnItemDataBound="grdList_ItemDataBound" AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView DataKeyNames="RegistrationNo,TransactionNo,SequenceNo">           
            <Columns>
                <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TransactionNo" UniqueName="TransactionNo" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IsApprove" UniqueName="IsApprove" Visible="false">
                </telerik:GridBoundColumn>
                <%--<telerik:GridBoundColumn DataField="ExecutionDate" HeaderText="Execution Date" UniqueName="ExecutionDate"
                    SortExpression="ExecutionDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>--%>
                <telerik:GridNumericColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName">        
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="ChargeQuantity" HeaderText="Qty" UniqueName="ChargeQuantity"
                    SortExpression="ChargeQuantity">
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridNumericColumn>
                <telerik:GridBoundColumn DataField="SRItemUnit" HeaderText="Unit" UniqueName="SRItemUnit"
                    SortExpression="SRItemUnit">
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>    
                <telerik:GridBoundColumn DataField="SRSpecimenTypes" HeaderText="Specimen Type" UniqueName="SRSpecimenTypes"
                    SortExpression="SRSpecimenTypes">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>    
                <telerik:GridTemplateColumn UniqueName="Collection" HeaderText="Collection Method">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <telerik:RadComboBox ID="cboSRCollectMethod" runat="server" Width="200px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSRCollectMethod_ItemDataBound"
                            OnItemsRequested="cboSRCollectMethod_ItemsRequested">
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="datepickercollect" HeaderText="Specimen Collect Date">
                    <HeaderStyle Width="180px" HorizontalAlign="left" />
                    <ItemStyle HorizontalAlign="left" />
                    <ItemTemplate>
                        <div style="display: flex; align-items: center;">
                            <telerik:RadDatePicker ID="txtSpecimenCollectDateTime" runat="server" Width="90px">
                            </telerik:RadDatePicker>
                            <telerik:RadTimePicker ID="txtSpecimenCollectTime" runat="server" Width="90px"></telerik:RadTimePicker>
                         </div>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="datepickerreceived" HeaderText="Specimen Received Date">
                    <HeaderStyle Width="180px" HorizontalAlign="left" />
                    <ItemStyle HorizontalAlign="left" />
                    <ItemTemplate>
                        <div style="display: flex; align-items: center;">
                            <telerik:RadDatePicker ID="txtSpecimenReceiveDateTime" runat="server" Width="90px">
                            </telerik:RadDatePicker>
                            <telerik:RadTimePicker ID="txtSpecimenReceiveTime" runat="server" Width="90px"></telerik:RadTimePicker>
                         </div>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>              
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="True">
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
