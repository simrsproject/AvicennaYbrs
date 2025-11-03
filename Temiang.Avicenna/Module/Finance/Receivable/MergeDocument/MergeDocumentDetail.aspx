<%@  Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="MergeDocumentDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Receivable.MergeDocumentDetail" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">            

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.mode != null) {
                    __doPostBack("<%= grdDocument.UniqueID %>", "rebindDocument");
                }
            }

            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }

            function openWinGuarantorInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var oguar = $find("<%= txtGuarantorID.ClientID %>");
                var lblToBeUpdate = "<%= lblGuarantorInfo.ClientID %>";
                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Finance/Master/GuarantorInfo/GuarantorInfoDialog.aspx?id=' + oguar.get_value() + '&lblGuarantorInfo=' + lblToBeUpdate + '")%>');
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }            

            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();

                switch (val) {
                    case "list":
                        location.replace('MergeDocumentList.aspx');
                        break;
                    case "reload":
                        __doPostBack("<%= grdDocument.UniqueID %>", "rebindAll");
                        break;                   
                }
            }

            function ReloadDocument() {
                __doPostBack("<%= grdDocument.UniqueID %>", "rebindDocument");
            }

            function MergeDocument() {
                __doPostBack("<%= grdDocument.UniqueID %>", "mergeDocuments");
            }
            
            function DocumentFileAction(type, url) {
                window.open(url, '_blank');
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>          
            <telerik:AjaxSetting AjaxControlID="RadToolBar2">
                <UpdatedControls>                  
                    <telerik:AjaxUpdatedControl ControlID="grdDocument" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDocument">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocument" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>                 
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px" OnClientClose="onClientClose"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton ID="RadToolBarButton1" runat="server" Text="List" Value="list" ImageUrl="~/Images/Toolbar/details16.png"
                HoveredImageUrl="~/Images/Toolbar/details16_h.png" DisabledImageUrl="~/Images/Toolbar/details16_d.png" />
            <telerik:RadToolBarButton ID="RadToolBarButton2" runat="server" Text="Refresh" Value="reload" ImageUrl="~/Images/Toolbar/refresh16.png"
                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 42%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="150px" MaxLength="20"
                                            ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="150px" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp; <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();"
                                        class="noti_Container">
                                        <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                            Text=""></asp:Label>&nbsp; </a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>                    
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="245px" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="25px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="198px" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <telerik:RadNumericTextBox ID="txtAgeInYear" runat="server" Width="49px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Y&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeInMonth" runat="server" Width="49px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;M&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeInDay" runat="server" Width="49px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;D
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="20"
                                            ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblServiceUnitName" runat="server" CssClass="labeldescription" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtRoomID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRoomName" runat="server" CssClass="labeldescription" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <telerik:RadNumericTextBox ID="txtTariffDiscForRoomIn" runat="server" Width="100px"
                                ReadOnly="true" Visible="False" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBedID" runat="server" Text="Bed"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtBedID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkIsRoomIn" Enabled="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
            <td style="width: 42%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">Coverage Class
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtCoverageClass" runat="server" Width="100px" MaxLength="20"
                                            ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCoverageClassName" runat="server" CssClass="labeldescription" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClassID" runat="server" Text="Charge Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtClassID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblClassName" runat="server" CssClass="labeldescription" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhysician" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtParamedicID" runat="server" Width="100px" MaxLength="20"
                                            ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblParamedicName" runat="server" CssClass="labeldescription" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadTextBox ID="txtGuarantorID" runat="server" Width="100px" MaxLength="20"
                                                        ReadOnly="true" />
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblGuarantorName" runat="server" CssClass="labeldescription" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>&nbsp; <a href="javascript:void(0);" onclick="javascript:openWinGuarantorInfo();"
                                        class="noti_Container">
                                        <asp:Label CssClass="noti_bubble" runat="server" ID="lblGuarantorInfo" AssociatedControlID="txtGuarantorID"
                                            Text=""></asp:Label>&nbsp; </a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLengthOfStay" runat="server" Text="Length Of Stay"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <telerik:RadNumericTextBox ID="txtLengthOfStay" runat="server" Width="100px" ReadOnly="True">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Day(s)
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>                    
                </table>
            </td>
            <td style="width: 16%; vertical-align: top" rowspan="2">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="right">
                            <fieldset id="FieldSet1" style="width: 200px; min-height: 200px;">
                                <legend>Photo</legend>
                                <asp:Image runat="server" ID="imgPatientPhoto" Width="200px" Height="200px" />
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>         
        </tr>
    </table>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
        ShowBaseLine="true">
        <Tabs>            
            <telerik:RadTab runat="server" Text="Document" PageViewID="pgDocument" Selected="true"/>            
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" BorderColor="Gray">        
        <telerik:RadPageView ID="pgDocument" runat="server" Selected="true">
            <telerik:RadGrid ID="grdDocument" runat="server" OnNeedDataSource="grdDocument_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdDocument_DeleteCommand">
                <MasterTableView DataKeyNames="Name" CommandItemDisplay="Top">
                    <CommandItemTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="50%">&nbsp;&nbsp;&nbsp;
                                    <a href='#' onclick='javascript:MergeDocument();return false;'>
                                        <img src='../../../../Images/Toolbar/save16.png' border='0' title='Merge' /></a>
                                    &nbsp;Merge &nbsp;&nbsp;&nbsp;
                                </td>
                                <td width="50%" align="right">
                                    <a href='#' onclick='javascript:ReloadDocument();return false;'>
                                        <img src='../../../../Images/Toolbar/refresh16.png' border='0' title='Refresh' /></a>
                                    &nbsp;Refresh &nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedStateDocuments" AutoPostBack="True"
                                    runat="server" Checked="false"></asp:CheckBox>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="detailChkbox" runat="server" Checked="False"></asp:CheckBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="Order" HeaderStyle-Width="50px" HeaderText="Order" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="orderTextBox" runat="server" Width="96%" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MaxLength="2"></telerik:RadNumericTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="CommandTemplateColumn" HeaderStyle-Width="40px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"DocumentFileAction(1, '{0}'); return false;\"><img src=\"../../../../Images/Toolbar/print_preview16.png\" border=\"0\" title=\"Open\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "Url"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Name" UniqueName="Name" HeaderText="File Name"
                            SortExpression="Name">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Path" UniqueName="Path" HeaderText="File Path"
                            SortExpression="Path">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Size" UniqueName="Size" HeaderText="File Size"
                            SortExpression="Size">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Accessed" HeaderText="Accessed Date/Time" UniqueName="Accessed"
                            SortExpression="Accessed" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete" Visible="false"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>       
    </telerik:RadMultiPage>
</asp:Content>
