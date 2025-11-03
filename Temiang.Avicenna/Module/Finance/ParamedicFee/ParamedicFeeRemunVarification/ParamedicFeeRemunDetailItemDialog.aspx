<%@ Page Title="Fee Detail By Item" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" 
CodeBehind="ParamedicFeeRemunDetailItemDialog.aspx.cs" 
Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.ParamedicFeeRemunDetailItemDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow runat="server" ID="winDialog" Animation="None" Behaviors="Maximize, Move, Close"
        Width="1000px" Height="600px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true"/>
   <telerik:RadGrid ID="grdRemunParamedic" runat="server" OnNeedDataSource="grdRemunParamedic_NeedDataSource" AutoGenerateColumns="False" GridLines="None">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo" PageSize="10">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="MedicalNo" HeaderText="MedicalNo"
                            UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn DataField="ExecutionDate" HeaderText="Transaction Date"
                            UniqueName="ExecutionDate" SortExpression="ExecutionDate" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" ></telerik:GridDateTimeColumn>

                        <telerik:GridBoundColumn DataField="Qty" HeaderText="Qty" UniqueName="Qty"
                            SortExpression="Qty" DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
    <br /><br /><br />
</asp:Content>