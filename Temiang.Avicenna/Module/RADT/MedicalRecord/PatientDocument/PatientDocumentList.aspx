<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="PatientDocumentList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.PatientDocumentList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script type="text/javascript" language="javascript">
            function openPatientDocument(patid) {
                var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/PatientDocument/PatientDocumentHist.aspx?progid=<%= ProgramID %>&patid=' + patid;
                openWinMaxWindow(url);
            }

            function openWinMaxWindow(url) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth);

                openWindow(url, width - 40, height - 40);
            }

            function batchUpload() {
                var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/PatientDocument/PatientDocumentBatchUpload.aspx';
                openWinMaxWindow(url);
            }
            function openWindow(url, width, height) {
                var oWnd = $find("<%= winEntry.ClientID %>");
                oWnd.setUrl(url);
                oWnd.setSize(width, height);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
                oWnd.show();
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winEntry" Width="600px" Height="600px" runat="server"
        ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" VisibleStatusbar="False" Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchMedicalNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchPatientName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchDateOfBirth">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchPhoneNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchAddress">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchParentSpouseName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblMedicalNoSearch" runat="server" Text="Medical No / Registration No"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtMedicalNoSearch" runat="server" Width="145px" MaxLength="11">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td style="width:5px"></td>
                                        <td>
                                            <telerik:RadTextBox ID="txtRegistrationNoSearch" runat="server" Width="150px" MaxLength="20">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchMedicalNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientNameSearch" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientNameSearch" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchPatientName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDateOfBirth" runat="server" Text="Date Of Birth"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px">
                                </telerik:RadDatePicker>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchDateOfBirth" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>

                    </table>
                </td>
                <td width="50%" valign="top">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchPhoneNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchAddress" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParentSpouseName" runat="server" Text="Parent / Spouse Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtParentSpouseName" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchParentSpouseName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td>
                    <a href="#" onclick="javascript:batchUpload(); return false;">
                        <img src="../../../../Images/BatchProcess80.png" border="0" alt="New" /><br />
                        Batch Upload</a>
                </td>
                <td></td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AllowPaging="true" PageSize="15" AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="PatientID" AllowNaturalSort="true" AllowSorting="true">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans" HeaderText="">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"javascript:openPatientDocument('{0}'); return false;\"><img src=\"{1}/Images/doc_upload16.png\" border=\"0\" title=\"View\" /></a>",
                                DataBinder.Eval(Container.DataItem, "PatientID"),Temiang.Avicenna.Common.Helper.UrlRoot())%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="PatientID" HeaderText="Patient ID" UniqueName="PatientID"
                    SortExpression="PatientID" HeaderStyle-Width="100px" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                    SortExpression="MedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
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
                <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DateOfBirth" HeaderText="Date Of Birth" UniqueName="DateOfBirth"
                    SortExpression="DateOfBirth" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AgeInString" HeaderText="Age" UniqueName="AgeInString"
                    SortExpression="AgeInString">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                    SortExpression="Address">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PhoneNo" HeaderText="Phone No" UniqueName="PhoneNo"
                    SortExpression="PhoneNo">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MobilePhoneNo" HeaderText="Mobile Phone No" UniqueName="MobilePhoneNo"
                    SortExpression="MobilePhoneNo">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OldMedicalNo" HeaderText="Old Medical No" UniqueName="OldMedicalNo"
                    SortExpression="OldMedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
