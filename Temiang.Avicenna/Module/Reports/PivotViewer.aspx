<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PivotViewer.aspx.cs" Inherits="Temiang.Avicenna.Module.Reports.PivotViewer" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v21.2" Namespace="DevExpress.Web.ASPxPivotGrid"
    TagPrefix="dxwpg" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dxpgw" Namespace="DevExpress.Web.ASPxPivotGrid" Assembly="DevExpress.Web.ASPxPivotGrid.v21.2, Version=21.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pivot Viewer</title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadSkinManager ID="skinManager" runat="server" Skin="WebBlue">
        </telerik:RadSkinManager>
        <telerik:RadScriptManager ID="scriptManager" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="ajaxManager" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="cboSummaryType">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pivotGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="chkShowRowGrandTotals">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pivotGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="chkShowColumnTotals">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pivotGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="chkShowColumnGrandTotals">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pivotGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="chkShowRowTotals">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pivotGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="chkShowGrandTotalsForSingleValues">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pivotGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="chkShowTotalsForSingleValues">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pivotGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadMenu runat="server" ID="radMenu" Style="float: none;" ClickToOpen="true">
            <Items>
                <telerik:RadMenuItem Text="Save Layout">
                    <Items>
                        <telerik:RadMenuItem>
                            <ItemTemplate>
                                <table width="400px">
                                    <tr>
                                        <td class="label" colspan="2">
                                            <asp:Label ID="lblsaveTo" runat="server" Text="Save Layout As"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="entry">
                                        <td>
                                            <telerik:RadTextBox ID="txtCustomPivotName" runat="server" Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSavePivot" runat="server" Text="Save" OnClick="btnSavePivot_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Show Summary">
                    <Items>
                        <telerik:RadMenuItem>
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td>Field</td>
                                        <td>
                                            <telerik:RadComboBox Width="100px" ID="cboFieldName" Style="z-index: 10001" runat="server">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="5px"></td>
                                        <td>Type</td>
                                        <td>
                                            <telerik:RadComboBox Width="100px" ID="cboSummaryType" Style="z-index: 10001" runat="server"
                                                AutoPostBack="true" OnSelectedIndexChanged="cboSummaryType_SelectedIndexChanged">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                                <fieldset>
                                    <legend>Show Totals</legend>
                                    <table width="200px">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkShowRowGrandTotals" runat="server" Text="Row Grand Totals" AutoPostBack="true"
                                                    OnCheckedChanged="chkShowRowGrandTotals_CheckedChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkShowColumnTotals" runat="server" Text="Column Totals" AutoPostBack="true"
                                                    OnCheckedChanged="chkShowColumnTotals_CheckedChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkShowColumnGrandTotals" runat="server" Text="Column Grand Totals" AutoPostBack="true"
                                                    OnCheckedChanged="chkShowColumnGrandTotals_CheckedChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkShowRowTotals" runat="server" Text="Row Totals" AutoPostBack="true"
                                                    OnCheckedChanged="chkShowRowTotals_CheckedChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkShowGrandTotalsForSingleValues" runat="server" Text="Grand Totals ForSingle Values"
                                                    AutoPostBack="true" OnCheckedChanged="chkShowGrandTotalsForSingleValues_CheckedChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkShowTotalsForSingleValues" runat="server" Text="Totals For Single Values"
                                                    AutoPostBack="true" OnCheckedChanged="chkShowTotalsForSingleValues_CheckedChanged" />
                                            </td>
                                        </tr>
                                    </table>

                                </fieldset>
                                <br />
                            </ItemTemplate>
                        </telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Export">
                    <Items>
                        <telerik:RadMenuItem>
                            <ItemTemplate>
                                <table width="450px">
                                    <tr>
                                        <td style="width:150px">
                                            <strong>Export Mode : </strong>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cboExportType" Style="z-index: 10001" Width="200px" runat="server"
                                                AutoPostBack="false">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <strong>Field Header Options : </strong>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkPrintHeadersOnEveryPage" runat="server" Text="Print Headers on every page" /><br />
                                            <asp:CheckBox ID="chkPrintFilterHeaders" runat="server" Text="Print filter field headers" Checked="True" /><br />
                                            <asp:CheckBox ID="chkPrintColumnHeaders" runat="server" Text="Print column field headers" Checked="True" /><br />
                                            <asp:CheckBox ID="chkPrintRowHeaders" runat="server" Text="Print row field headers" Checked="True" /><br />
                                            <asp:CheckBox ID="chkPrintDataHeaders" runat="server" Text="Print data field headers" Checked="True" /><br /><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <strong>Field Values Options : </strong>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkPrintColumnAreaOnEveryPage" runat="server" Text="Print column area on every page" /><br />
                                            <asp:CheckBox ID="chkPrintRowAreaOnEveryPage" runat="server" Text="Print row area on every page" /><br />
                                            <asp:CheckBox ID="chkMergeColumnFieldValues" runat="server" Text="Merge values of outer column fields" Checked="True"/><br />
                                            <asp:CheckBox ID="chkMergeRowFieldValues" runat="server" Text="Merge values of outer row fields" Checked="True"/><br />
                                            <asp:CheckBox ID="checkCustomFormattedValuesAsText" runat="server" Text="Export custom formatted values as text (Excel Option)" Checked="True"/><br /><br />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td valign="top"><strong>Data Aware Options :</strong></td>
                                        <td>
                                            <asp:CheckBox ID="chkDaAllowGrouping" runat="server" Text="Allow grouping columns/rows" Checked="true" /><br />
                                            <asp:CheckBox ID="chkDaAllowFixedColumns" runat="server" Text="Allow fixed ColumnArea and RowArea" Checked="true"/><br />
                                            <asp:CheckBox ID="chkDaExportCellValuesAsText" runat="server" Text="Export cell values as display text" /><br />
                                            <asp:CheckBox ID="chkDaExportRawData" runat="server" Text="Export raw data" /><br /><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button runat="server" ID="btnExport" Text="Export" OnClick="btnExport_Click" /></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenu>
        <div style="height: 5px">
        </div>
        <dxwpg:ASPxPivotGrid ID="pivotGrid" runat="server" >
            <OptionsView ShowFilterHeaders="true" ShowDataHeaders="False" HorizontalScrollBarMode="Visible" HorizontalScrollingMode="Standard"/>
        </dxwpg:ASPxPivotGrid>

        <dxpgw:ASPxPivotGridExporter ID="pivotGridExporter" runat="server" ASPxPivotGridID="pivotGrid" />
    </form>
</body>
</html>
