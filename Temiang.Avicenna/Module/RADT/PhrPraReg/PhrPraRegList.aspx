<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="PhrPraRegList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PhrPraRegList" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .viewWrap {
            padding: 5px;
            background-color: gray;
        }

        .childMenu {
            padding: 5px 5px;
            background-color: rgb(54, 72, 91);
            height: 17px;
        }
    </style>


    <script type="text/javascript">


    </script>

    <telerik:RadCodeBlock runat="server" ID="codeBlock">

        <script type="text/javascript">
            function entryPhrPraReg(md, id, regno, fid, unit, patid, grdId) {
                var url = 'PhrPraRegDetail.aspx?md=' + md + '&id=' + id + '&regno=' + regno + '&unit=' + unit + '&fid=' + fid + '&menu=su' + '&patid=' + patid + '&ccm=rebind&cet=<%=grdList.ClientID%>';
                window.openWinEntryMaxWindow(url);
            }
            function openWinEntryMaxWindow(url) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth);

                if (!(url.includes("&rt=") || url.includes("?rt=")))	
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';

                openWindow(url, width - 40, height - 40);
            }
            function openWindow(url, width, height) {
                var oWnd;
                oWnd = radopen(url, 'winDialog');
                oWnd.setSize(width, height);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
            }

            function radWindowManager_ClientClose(oWnd, args) {
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
            var delay = (function () {
                var timer = 0;
                return function (callback, ms) {
                    clearTimeout(timer);
                    timer = setTimeout(callback, ms);
                };
            })();
            function QuickSearchPatientKeypress(sender, args) {
                var c = args.get_keyCode();
                if (c == 13) {
                    ApplyQuickSearchPatient();
                }
                else {
                    delay(function () { ApplyQuickSearchPatient(); }, 1000);
                }
            }
            function ApplyQuickSearchPatient() {
                __doPostBack("<%=grdList.UniqueID %>", "rebind:");
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="radWindowManager_ClientClose">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" Modal="True">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>
    <cc:CollapsePanel ID="CollapsePanel2" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientSearch" runat="server" Text="Patient Name / Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtPatientSearch" Width="300px">
                                    <ClientEvents OnKeyPress="QuickSearchPatientKeypress"></ClientEvents>
                                </telerik:RadTextBox>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchPatient" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDateOfBirth" runat="server" Text="Date Of Birth"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchDateOfBirth" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="100px">
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
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
        AllowSorting="true" OnItemCreated="grdList_ItemCreated" OnItemCommand="grdList_ItemCommand" OnItemDataBound="grdList_OnItemDataBound">
        <MasterTableView DataKeyNames="PatientID" ClientDataKeyNames="PatientID"
            GroupLoadMode="Server" HierarchyLoadMode="ServerOnDemand">
            <NestedViewTemplate>
                <asp:Panel runat="server" ID="pnlNestedView" CssClass="viewWrap" Visible="false">
                    <telerik:RadTabStrip runat="server" ID="tabStrip" MultiPageID="Multipage1" SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Health Record" PageViewID="pvPHR" />
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage runat="server" ID="Multipage1" SelectedIndex="0" RenderSelectedPageOnly="false"
                        BorderColor="Black" BorderStyle="Solid">
                        <telerik:RadPageView runat="server" ID="pvPHR">
                            <telerik:RadGrid ID="grdForm" runat="server"
                                AutoGenerateColumns="False" GridLines="None">
                                <MasterTableView DataKeyNames="QuestionFormID" ShowHeader="True">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
                                            <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                            <ItemTemplate>
                                                <%# PhrLink(Container) %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="TransactionNo" HeaderText="" HeaderStyle-Width="30px">
                                            <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                            <ItemTemplate>
                                                <%#Eval("TransactionNo")==DBNull.Value || string.IsNullOrEmpty(Eval("TransactionNo").ToString() )? string.Empty:string.Format("<a href=\"#\" onclick=\"printPreviewQuestionForm( '{0}','{1}','{2}'); return false;\"><img src=\"../../../Images/Toolbar/print16.png\" border=\"0\" /></a>", Eval("TransactionNo"), Eval("RegistrationNo"), Eval("QuestionFormID"))%>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="PatientID" HeaderText="PatientID" UniqueName="PatientID" SortExpression="PatientID" HeaderStyle-Width="80px">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RmNO" HeaderText="Form ID" UniqueName="RmNO" SortExpression="RmNO" HeaderStyle-Width="80px">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="QuestionFormName" HeaderText="Form Name" UniqueName="QuestionFormName" SortExpression="QuestionFormName" HeaderStyle-Width="200px">
                                            <ItemTemplate>
                                                <%# Eval("QuestionFormName") %>&nbsp;&nbsp;&nbsp;<%# AddPhrLink(Container) %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="TransactionNo" HeaderText="Document No" HeaderStyle-Width="150px">
                                            <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                            <ItemTemplate>
                                                <%# PhrViewLink(Container, "trno") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="CreateByUserID" HeaderText="Created By" UniqueName="CreateByUserID"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="False">
                                    <Selecting AllowRowSelect="False" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </asp:Panel>
            </NestedViewTemplate>
            <Columns>

                <telerik:GridBoundColumn DataField="Salutation" HeaderText="Salutation" UniqueName="Salutation"
                    SortExpression="Salutation">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                    SortExpression="MedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OldMedicalNo" HeaderText="Old Medical No" UniqueName="OldMedicalNo"
                    SortExpression="OldMedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="DateOfBirth" HeaderText="Date Of Birth" UniqueName="DateOfBirth"
                    SortExpression="DateOfBirth">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                    SortExpression="Address">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PhoneNo" HeaderText="Phone No" UniqueName="PhoneNo"
                    SortExpression="PhoneNo">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MobilePhoneNo" HeaderText="Mobile Phone No" UniqueName="MobilePhoneNo"
                    SortExpression="MobilePhoneNo">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
