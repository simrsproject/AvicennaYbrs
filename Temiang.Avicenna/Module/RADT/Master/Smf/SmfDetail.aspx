<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="SmfDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.SmfDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSmfID" runat="server" Text="SMF ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radtextbox id="txtSmfID" runat="server" width="100px" maxlength="10" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSmfID" runat="server" ErrorMessage="SMF ID required."
                                ValidationGroup="entry" ControlToValidate="txtSmfID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSmfName" runat="server" Text="SMF Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radtextbox id="txtSmfName" runat="server" width="300px" maxlength="100" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSmfName" runat="server" ErrorMessage="SMF Name required."
                                ValidationGroup="entry" ControlToValidate="txtSmfName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRParamedicFeeCaseType" runat="server" Text="Physician Fee Case Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radcombobox id="cboSRParamedicFeeCaseType" runat="server" width="300px">
                            </telerik:radcombobox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRParamedicFeeCaseType" runat="server" ErrorMessage="Physician Fee Case Type required."
                                ControlToValidate="cboSRParamedicFeeCaseType" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRAssessmentType" runat="server" Text="Assessment Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radcombobox id="cboSRAssessmentType" runat="server" width="300px" allowcustomtext="true"
                                filter="Contains" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSmfBackcolor" runat="server" Text="Backcolor in Bed Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:radcolorpicker showicon="true" id="txtSmfBackcolor" runat="server" width="300px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
    <telerik:radtabstrip id="tabMain" runat="server" multipageid="mpgMain" selectedindex="0">
        <tabs>
            <telerik:radtab runat="server" text="Item Service & Package" pageviewid="pgvItemServiveDetail" />
            <telerik:radtab runat="server" text="Diagnose" pageviewid="pgvDiagnoseDetail" />

        </tabs>
    </telerik:radtabstrip>
    <telerik:radmultipage id="mpgMain" runat="server" selectedindex="0" borderstyle="Solid"
        bordercolor="gray">
        <telerik:radpageview id="pgvItemServiveDetail" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFilterItemService" runat="server" Text="Item ID / Item Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:radtextbox id="txtFilterItemService" runat="server" width="300px" maxlength="100" />
                                </td>
                                <td width="20">
                                    <asp:ImageButton ID="btnFilterItemService" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterItemService_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <telerik:radgrid id="grdSmfItemService" runat="server" onneeddatasource="grdSmfItemService_NeedDataSource"
                autogeneratecolumns="False" gridlines="None" onupdatecommand="grdSmfItemService_UpdateCommand"
                ondeletecommand="grdSmfItemService_DeleteCommand" oninsertcommand="grdSmfItemService_InsertCommand"
                allowpaging="true">
                <headercontextmenu>
                </headercontextmenu>
                <mastertableview commanditemdisplay="None" datakeynames="SmfID, ItemID" pagesize="15"
                    pagerstyle-mode="NextPrevNumericAndAdvanced">
                    <commanditemtemplate>
                        &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdSmfItemService.MasterTableView.IsItemInserted %>'>
                                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                                    &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                                </asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lbPickListPA" runat="server" Visible="False" OnClientClick="javascript:openWinPickListPA();return false;">
                                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />
                                    &nbsp;<asp:Label runat="server" ID="Label1" Text="Account Mapping (Item Product)"></asp:Label>
                                </asp:LinkButton>
                    </commanditemtemplate>
                    <commanditemstyle height="29px" />
                    <columns>
                        <telerik:grideditcommandcolumn buttontype="ImageButton">
                            <headerstyle width="35px" />
                            <itemstyle cssclass="MyImageButton" />
                        </telerik:grideditcommandcolumn>
                        <telerik:gridboundcolumn headerstyle-width="100px" datafield="ItemID" headertext="Item ID"
                            uniquename="ItemID" sortexpression="ItemID" headerstyle-horizontalalign="Left"
                            itemstyle-horizontalalign="Left" />
                        <telerik:gridboundcolumn datafield="ItemName" headertext="Item Name" uniquename="ItemName"
                            sortexpression="ItemName" headerstyle-horizontalalign="Left" itemstyle-horizontalalign="Left" />
                        <telerik:gridcheckboxcolumn headerstyle-width="80px" datafield="IsVisible" headertext="Visible"
                            uniquename="IsVisible" sortexpression="IsVisible" headerstyle-horizontalalign="Center"
                            itemstyle-horizontalalign="Center" />
                        <telerik:gridbuttoncolumn uniquename="DeleteColumn" text="Delete" commandname="Delete"
                            buttontype="ImageButton" confirmtext="Delete this row?">
                            <headerstyle width="35px" />
                            <itemstyle horizontalalign="Center" cssclass="MyImageButton" />
                        </telerik:gridbuttoncolumn>
                    </columns>
                    <editformsettings usercontrolname="SmfItemServiceDetail.ascx" editformtype="WebUserControl">
                        <editcolumn uniquename="SmfItemServiceEditCommand">
                        </editcolumn>
                    </editformsettings>
                </mastertableview>
                <filtermenu>
                </filtermenu>
                <clientsettings enablerowhoverstyle="true">
                    <resizing allowcolumnresize="True" />
                    <selecting allowrowselect="false" />
                </clientsettings>
            </telerik:radgrid>
        </telerik:radpageview>

        <telerik:radpageview id="pgvDiagnoseServiveDetail" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFilterDiagnose" runat="server" Text="Diagnose ID / Diagnose Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:radtextbox id="txtFilterDiagnose" runat="server" width="300px" maxlength="100" />
                                </td>
                                <td width="20">
                                    <asp:ImageButton ID="btnFilterDiagnose" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterDiagnose_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <telerik:radgrid id="grdSmfDiagnose" runat="server" onneeddatasource="grdSmfDiagnose_NeedDataSource"
                autogeneratecolumns="False" gridlines="None" onupdatecommand="grdSmfDiagnose_UpdateCommand"
                ondeletecommand="grdSmfDiagnose_DeleteCommand" oninsertcommand="grdSmfDiagnose_InsertCommand"
                allowpaging="true">
                <headercontextmenu>
                </headercontextmenu>
                <mastertableview commanditemdisplay="None" datakeynames="SmfID, DiagnoseID" pagesize="15"
                    pagerstyle-mode="NextPrevNumericAndAdvanced">
                    <commanditemtemplate>
                        &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdSmfDiagnose.MasterTableView.IsItemInserted %>'>
                                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                                    &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                                </asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lbPickListPA" runat="server" Visible="False" OnClientClick="javascript:openWinPickListPA();return false;">
                                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />
                                    &nbsp;<asp:Label runat="server" ID="Label1" Text="Account Mapping (Diagnose Product)"></asp:Label>
                                </asp:LinkButton>
                    </commanditemtemplate>
                    <commanditemstyle height="29px" />
                    <columns>
                        <telerik:grideditcommandcolumn buttontype="ImageButton">
                            <headerstyle width="35px" />
                            <itemstyle cssclass="MyImageButton" />
                        </telerik:grideditcommandcolumn>
                        <telerik:gridboundcolumn headerstyle-width="100px" datafield="DiagnoseID" headertext="Diagnose ID"
                            uniquename="DiagnoseID" sortexpression="DiagnoseID" headerstyle-horizontalalign="Left"
                            itemstyle-horizontalalign="Left" />
                        <telerik:gridboundcolumn datafield="DiagnoseName" headertext="Diagnose Name" uniquename="DiagnoseName"
                            sortexpression="DiagnoseName" headerstyle-horizontalalign="Left" itemstyle-horizontalalign="Left" />
                        <telerik:gridcheckboxcolumn headerstyle-width="80px" datafield="IsVisible" headertext="Visible"
                            uniquename="IsVisible" sortexpression="IsVisible" headerstyle-horizontalalign="Center"
                            itemstyle-horizontalalign="Center" />
                        <telerik:gridbuttoncolumn uniquename="DeleteColumn" text="Delete" commandname="Delete"
                            buttontype="ImageButton" confirmtext="Delete this row?">
                            <headerstyle width="35px" />
                            <itemstyle horizontalalign="Center" cssclass="MyImageButton" />
                        </telerik:gridbuttoncolumn>
                    </columns>
                    <editformsettings usercontrolname="SmfDiagnoseDetail.ascx" editformtype="WebUserControl">
                        <editcolumn uniquename="SmfDiagnoseEditCommand">
                        </editcolumn>
                    </editformsettings>
                </mastertableview>
                <filtermenu>
                </filtermenu>
                <clientsettings enablerowhoverstyle="true">
                    <resizing allowcolumnresize="True" />
                    <selecting allowrowselect="false" />
                </clientsettings>
            </telerik:radgrid>
        </telerik:radpageview>

    </telerik:radmultipage>
</asp:Content>
