<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="MedicalRecordFileBorrowedList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.MedicalRecordFileBorrowedList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" OnItemDataBound="grdList_ItemDataBound">
        <MasterTableView Name="master" DataKeyNames="TransactionNo">
            <Columns>
                <telerik:GridTemplateColumn HeaderStyle-Width="140px" HeaderText="Transaction No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "TransactionNo")%>&nbsp;
                       
                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsWarnedVisible")) ? "<img src=\"../../../../Images/Animated/warning16.gif\" border=\"0\" />" : string.Empty%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MedicalNo" HeaderText="Medical No"
                    UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName">
                    <ItemTemplate>
                        <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DateOfBorrowing"
                    HeaderText="Borrowed Date" UniqueName="DateOfBorrowing" SortExpression="DateOfBorrowing"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ServiceUnitName" HeaderText="Service Unit"
                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="NameOfTheBorrower"
                    HeaderText="Borrower's Name" UniqueName="NameOfTheBorrower" SortExpression="NameOfTheBorrower"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Notes"
                    HeaderText="Notes" UniqueName="Notes" SortExpression="Notes"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" /> 
                <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="GivenBy"
                    HeaderText="Submitted By" UniqueName="GivenBy" SortExpression="GivenBy"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Duration" HeaderText="Duration Day(s)"
                    UniqueName="Duration" SortExpression="Duration" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ShouldBeReturnDate" HeaderText="Should Be Return"
                    UniqueName="ShouldBeReturnDate" SortExpression="ShouldBeReturnDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DateOfReturn" HeaderText="Returned Date"
                    UniqueName="DateOfReturn" SortExpression="DateOfReturn" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="ReceivedBy"
                    HeaderText="Received By" UniqueName="ReceivedBy" SortExpression="ReceivedBy"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="LoB" HeaderText="LoB"
                    UniqueName="LoB" SortExpression="LoB" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" Visible="false" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
