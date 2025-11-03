<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="PersonalInfoList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.PersonalInfoList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinGoogleFormImport() {
                var oWnd = $find("<%= winImport.ClientID %>");
                oWnd.setUrl("PersonalInfoGoogleFormImport.aspx?ccm=rebind&cet=<%=grdList.ClientID %>");
                oWnd.show();
            }
            function winImport_ClientClose(oWnd, args) {
                //get the transferred arguments from MasterDialogEntry
                var arg = args.get_argument();
                if (arg != null) {
                    if (arg.callbackMethod === 'submit') {
                        __doPostBack(arg.eventTarget, arg.eventArgument);
                    } else {

                        if (arg.callbackMethod === 'rebind') {
                            var ctl = $find(arg.eventTarget);
                            if (typeof ctl.rebind == 'function') {
                                ctl.rebind();
                            } else {
                                var masterTable = $find(arg.eventTarget).get_masterTableView();
                                masterTable.rebind();
                            }
                        }
                    }
                }

            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="1200px" Height="550px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        ReloadOnShow="true" ID="winImport" OnClientClose="winImport_ClientClose">
    </telerik:RadWindow>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="PersonID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PersonID" HeaderText="Person ID"
                    UniqueName="PersonID" SortExpression="PersonID" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                    UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                    SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Nik" HeaderText="Identity Number" UniqueName="Nik"
                    SortExpression="Nik" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PlaceBirth" HeaderText="Place Birth" UniqueName="PlaceBirth"
                    SortExpression="PlaceBirth" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="BirthDate" HeaderText="DoB"
                    UniqueName="BirthDate" SortExpression="BirthDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="GenderName" HeaderText="Gender"
                    UniqueName="GenderName" SortExpression="GenderName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ReligionName" HeaderText="Religion" UniqueName="ReligionName"
                    SortExpression="ReligionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="MaritalStatus" HeaderText="Marital Status" UniqueName="MaritalStatus"
                    SortExpression="MaritalStatus" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ClassName" HeaderText="Coverage Class" UniqueName="ClassName"
                    SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
