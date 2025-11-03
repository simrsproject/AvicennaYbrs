<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="DiagnosisAndProcedureList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.DiagnosisAndProcedureList" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoEditUrl(pid, cid, pmid, pst) {
                var url = 'DiagnosisAndProcedureDetail.aspx?md=edit&pid=' + pid + '&cid=' + cid + '&pmid=' + pmid + '&pst=' + pst;
                window.location.href = url;
            }

            function gotoViewUrl(pid, cid, pmid, pst) {
                var url = 'DiagnosisAndProcedureDetail.aspx?md=view&pid=' + pid + '&cid=' + cid + '&pmid=' + pmid + '&pst=' + pst;
                window.location.href = url;
            }

            function viewHistory(pid) {
                var oWnd = $find("<%= winHistory.ClientID %>");
                oWnd.setUrl('DiagnosisAndProcedureHistoryDetail.aspx?pid=' + pid);

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="600px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winHistory">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnRegDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchMedical">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchMedical">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchPatientName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchGuarantorID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnStatus">
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
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromDateSearch" runat="server" Text="Registration/Discharge Date (Inpatient)" />
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtFromDate" runat="server" Width="100px" />
                                        </td>
                                        <td>
                                            &nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtToDate" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnRegDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegNoSearch" runat="server" Text="Registration No" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchRegistration" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" AllowCustomText="true"
                                    Filter="Contains" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchMedical" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="LblPatientName" runat="server" Text="Patient Name" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchPatientName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboGuarantorID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                    AutoPostBack="False" MarkFirstMatch="false" EnableLoadOnDemand="true" NoWrap="False"
                                    OnItemDataBound="cboGuarantorID_ItemDataBound" OnItemsRequested="cboGuarantorID_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 30 result
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchGuarantorID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblStatus" runat="server" Text="Status" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboStatus" Width="300px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdPatient" runat="server" OnNeedDataSource="grdPatient_NeedDataSource"
        AllowPaging="true" AllowSorting="true" ShowStatusBar="true">
        <MasterTableView DataKeyNames="MedicalNo" PageSize="15" AutoGenerateColumns="false">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}','{2}','{3}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                DataBinder.Eval(Container.DataItem,"ServiceUnitID"),
                                cboParamedicID.SelectedValue, cboStatus.SelectedValue)%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (this.IsUserEditAble.Equals(false) ? string.Empty : string.Format("<a href=\"#\" onclick=\"gotoEditUrl('{0}','{1}','{2}','{3}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" /></a>",
                                DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                DataBinder.Eval(Container.DataItem,"ServiceUnitID"),
                                cboParamedicID.SelectedValue, cboStatus.SelectedValue))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                    SortExpression="RegistrationDate">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="RegistrationTime" HeaderText="Time"
                    UniqueName="RegistrationTime" SortExpression="RegistrationTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="MedicalNo" HeaderText="Medical No"
                    UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SalutationName" HeaderText=""
                    UniqueName="SalutationName" SortExpression="SalutationName" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName">
                    <ItemTemplate>
                        <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DateOfBirth" HeaderText="Date Of Birth"
                    UniqueName="DateOfBirth" SortExpression="DateOfBirth" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Phycisian Name" UniqueName="ParamedicName"
                    SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                    SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsConsul" HeaderText="Consul"
                    UniqueName="IsConsul" SortExpression="IsConsul" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsClosed" HeaderText="Closed"
                    UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsSoape" HeaderText="SOAP"
                    UniqueName="IsSoape" SortExpression="IsSoape" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsDiagnosis" HeaderText="Diag" HeaderTooltip="IsDiagnosis"
                    UniqueName="IsDiagnosis" SortExpression="IsDiagnosis" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsProcedure" HeaderText="Proc" HeaderTooltip="IsProcedure"
                    UniqueName="IsProcedure" SortExpression="IsProcedure" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsNewPatient" HeaderText="New" HeaderTooltip="New Patient"
                    UniqueName="IsNewPatient" SortExpression="IsNewPatient" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"viewHistory('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View History\"/></a>",
                                DataBinder.Eval(Container.DataItem, "PatientID"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true" />
    </telerik:RadGrid>
</asp:Content>
