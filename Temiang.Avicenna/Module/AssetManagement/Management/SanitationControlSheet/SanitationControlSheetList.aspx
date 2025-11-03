<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="SanitationControlSheetList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Management.SanitationControlSheetList" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            
            function gotoViewUrl(cno, fid) {
                var url = 'SanitationControlSheetDetail.aspx?md=view&id=' + cno + '&fid=' + fid;
                window.location.href = url;
            }

            function gotoAddUrl(fid) {
                var url = 'SanitationControlSheetDetail.aspx?md=new&fid=' + fid;
                window.location.href = url;
            }

        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" />
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterControlDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterQuestionFormID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRequest">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRequest" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Control Sheet" PageViewID="pgSheet"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Control Result" PageViewID="pgResult">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgSheet" runat="server" Selected="true">
            <telerik:RadGrid ID="grdControlSheet" runat="server" OnNeedDataSource="grdControlSheet_NeedDataSource" AllowPaging="true" PageSize="15"
                AutoGenerateColumns="false">
                <MasterTableView DataKeyNames="QuestionFormID" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="New" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"gotoAddUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"New\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "QuestionFormID"))) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="QuestionFormID" HeaderText="ID"
                            UniqueName="QuestionFormID" SortExpression="QuestionFormID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="QuestionFormName" HeaderText="Control Sheet Name" UniqueName="QuestionFormName"
                            SortExpression="QuestionFormName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgResult" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblControlDate" runat="server" Text="Control Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtControlFromDate" runat="server" Width="100px" />
                                            </td>
                                            <td></td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtControlToDate" runat="server" Width="100px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="text-align: left;">
                                    <asp:ImageButton ID="btnFilterControlDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblQuestionFormID" runat="server" Text="Control Sheet"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboQuestionFormID" runat="server" Width="300px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterQuestionFormID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AllowPaging="true" PageSize="15"
                AutoGenerateColumns="false">
                <MasterTableView DataKeyNames="ControlSheetNo" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="View" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "ControlSheetNo"), DataBinder.Eval(Container.DataItem, "QuestionFormID"))) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ControlSheetNo" HeaderText="Control Sheet No"
                            UniqueName="ControlSheetNo" SortExpression="ControlSheetNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        
                        <telerik:GridBoundColumn DataField="ControlDate" HeaderText="Control Date"
                            UniqueName="ControlDate" SortExpression="ControlDate" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="108px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="QuestionFormName" HeaderText="Control Sheet Name" UniqueName="QuestionFormName"
                            SortExpression="QuestionFormName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                            UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsVoid" HeaderText="Void"
                            UniqueName="IsApproved" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>