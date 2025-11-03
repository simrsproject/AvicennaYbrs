<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NursingCareStandardEditDiagnose.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare.NursingCareStandardEditDiagnose" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="gridListDiagnosa" runat="server" AutoGenerateColumns="false"
        GridLines="None" OnNeedDataSource="gridListDiagnosa_NeedDataSource">
        <MasterTableView ClientDataKeyNames="NursingDiagnosaID,ID" DataKeyNames="NursingDiagnosaID,ID"
            GroupLoadMode="Client">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="Priority" UniqueName="Priority">
                    <HeaderStyle HorizontalAlign="Left" Width="60px" />
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtDefaultPriority" runat="server" Text='<%#Eval("Priority")%>'
                            Width="40px" MaxLength="5" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Date (m/d/y)" UniqueName="Date">
                    <HeaderStyle HorizontalAlign="Left" Width="180px" />
                    <ItemTemplate>
                        <telerik:RadDateTimePicker ID="rdtDateDiag" runat="server" SelectedDate='<%# Eval("ExecuteDateTime") %>' DateInput-DisplayDateFormat="MM/dd/yyyy HH:mm">
                        </telerik:RadDateTimePicker>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="NursingDiagnosaName" HeaderText="Diagnosis" SortExpression="NursingDiagnosaName"
                    UniqueName="NursingDiagnosaName" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NursingDiagnosaParentID" HeaderText="Parent"
                    SortExpression="NursingDiagnosaParentID" UniqueName="NursingDiagnosaParentID"
                    Visible="false">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Diagnosis" UniqueName="EditedDiagnosis">
                    <HeaderStyle HorizontalAlign="Left" Width="220px" />
                    <ItemTemplate>
                        <telerik:RadTextBox runat="server" ID="txtNursingDiagnosaNameTransDT" Text='<%#Eval("NursingDiagnosaNameTransDT")%>' Width="200px" TextMode="MultiLine"></telerik:RadTextBox>    
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="NursingDiagnosaNameDisplay" HeaderText="Diagnosis"
                    SortExpression="NursingDiagnosaNameDisplay" UniqueName="NursingDiagnosaNameDisplay">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Objective Periode" UniqueName="EvalPeriod">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtDefaultEvalPeriod" runat="server" MinValue="0" 
                        Value='<%#System.Convert.ToDouble(Eval("EvalPeriod"))%>' Width="30px" >
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="" UniqueName="X">
                    <HeaderStyle HorizontalAlign="Left" Width="25px" />
                    <ItemTemplate>
                        <label id="label" runat="server">X</label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Factor (Hour(s))" UniqueName="PeriodConversionInHour">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtDefaultPeriodConversionInHour" runat="server" MinValue="1"
                        Value='<%#System.Convert.ToDouble(Eval("PeriodConversionInHour"))%>' Width="30px" >
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                        <label id="labelHours" runat="server">Hour(s)</label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
