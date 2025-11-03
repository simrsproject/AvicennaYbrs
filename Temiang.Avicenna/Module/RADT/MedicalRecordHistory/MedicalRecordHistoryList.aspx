<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="MedicalRecordHistoryList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecordHistoryList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function gotoViewUrl(recID) {
            var url = 'MedicalRecordHistoryDetail.aspx?patientID=' + recID;
            window.location.href = url;
        }
    </script>

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchMedicalNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchPatient">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchBirthDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchAddress">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPatient">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" MaxLength="15">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchMedicalNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientSearch" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientSearch" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchPatient" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBirthDate" runat="server" Text="Birth Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtBirthDate" runat="server" Width="100px">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchBirthDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Patient Address"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px" TextMode="MultiLine">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchAddress" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdPatient" runat="server" OnNeedDataSource="grdPatient_NeedDataSource"
        AutoGenerateColumns="False" AllowPaging="True" PageSize="15" AllowSorting="True"
        GridLines="None">
        <MasterTableView DataKeyNames="PatientID">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}'); return false;\"><img src=\"../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"View\"/></a>",
                                DataBinder.Eval(Container.DataItem, "PatientID")) %>
                    </ItemTemplate>
                    <HeaderStyle Width="20px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="PatientID" HeaderText="Patient ID" UniqueName="PatientID"
                    SortExpression="PatientID" Visible="False">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                    SortExpression="MedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SalutationName" HeaderText=""
                    UniqueName="SalutationName" SortExpression="SalutationName" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />

                <telerik:GridTemplateColumn HeaderStyle-Width="400px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName">
                    <ItemTemplate>
                        <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsAlive")) ? string.Empty : "<img src=\"../../../Images/Rip16.png\" border=\"0\" alt=\"Decease\" title=\"Decease\" />" %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DateOfBirth" HeaderText="Date Of Birth" UniqueName="DateOfBirth"
                    SortExpression="DateOfBirth" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                    SortExpression="Address">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OldMedicalNo" HeaderText="Old Medical No" UniqueName="OldMedicalNo"
                    SortExpression="OldMedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings AllowDragToGroup="true" EnableRowHoverStyle="true" AllowExpandCollapse="true">
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
