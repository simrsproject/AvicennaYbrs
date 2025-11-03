<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustomNoMenu.Master"
    AutoEventWireup="true" CodeBehind="Kiosk.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.OutPatient.Kiosk" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <style type="text/css">
            .scroll
            {
            	height: 540px;
                width: 100%;/*540px;*/
                /*position: absolute;*/
                overflow-y: scroll;
            }
            .schinfo
            {
                font-size: small;
            }
            
            .scroll::-webkit-scrollbar {
                width: 50px;
            }
             
            .scroll::-webkit-scrollbar-track {
                -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3); 
                border-radius: 30px;
            }
             
            .scroll::-webkit-scrollbar-thumb {
                border-radius: 30px;
                -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.5); 
            }

            .btnLang {
                Width: 180px; 
                Height: 60px; 
            }
            
            .btnNo {
                Width: 95px; 
                Height: 70px; 
                font-size: x-large;
            }
            
            .btnRegister {
            	Width: 150px; 
                Height: 70px; 
            }
        </style>
        <script language="javascript" type="text/javascript">
            function NumberPush(obj) {
                //console.log(obj);
                //alert(obj.value);
                $find('<%=txtMedicalNo.ClientID %>').set_value($find('<%=txtMedicalNo.ClientID %>').get_value() + '' + obj.value);
                //txtMedicalNo
            }

            var txt = "";
            function SelectMRN() {
                var txtMrn = $find('<%=txtMedicalNo.ClientID %>');
                //console.log(txtMrn);
                txtMrn.focus();
                setTimeout('SelectMRN()', 1000);
            }

            $telerik.$(document).ready(function () {
                setTimeout(SelectMRN(), 1000);
            });
            function isKeyPressed(event, expectedKey, expectedCode) {
                const code = event.which || event.keyCode;

                if (expectedKey === event.key || code === expectedCode) {
                    return true;
                }
                return false;
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnOk">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnOk" />
                    <telerik:AjaxUpdatedControl ControlID="pnlContent" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMessage" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMain" />
                    <telerik:AjaxUpdatedControl ControlID="listPoli" />
                    <telerik:AjaxUpdatedControl ControlID="listQueue" />
                    <telerik:AjaxUpdatedControl ControlID="GridRegistered" />
                    <telerik:AjaxUpdatedControl ControlID="listDokter" />
                    <telerik:AjaxUpdatedControl ControlID="MultipleAppt" />
                    <telerik:AjaxUpdatedControl ControlID="pnlPatientInfo" />
                    <telerik:AjaxUpdatedControl ControlID="hfServiceUnitID" />
                    <telerik:AjaxUpdatedControl ControlID="hfParamedicID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="listPoli">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlContent" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMessage" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMain" />
                    <telerik:AjaxUpdatedControl ControlID="listPoli" />
                    <telerik:AjaxUpdatedControl ControlID="listQueue" />
                    <telerik:AjaxUpdatedControl ControlID="GridRegistered" />
                    <telerik:AjaxUpdatedControl ControlID="listDokter" />
                    <telerik:AjaxUpdatedControl ControlID="MultipleAppt" />
                    <telerik:AjaxUpdatedControl ControlID="pnlPatientInfo" />
                    <telerik:AjaxUpdatedControl ControlID="hfServiceUnitID" />
                    <telerik:AjaxUpdatedControl ControlID="hfParamedicID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="listDokter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlContent" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMessage" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMain" />
                    <telerik:AjaxUpdatedControl ControlID="listPoli" />
                    <telerik:AjaxUpdatedControl ControlID="listQueue" />
                    <telerik:AjaxUpdatedControl ControlID="GridRegistered" />
                    <telerik:AjaxUpdatedControl ControlID="listDokter" />
                    <telerik:AjaxUpdatedControl ControlID="MultipleAppt" />
                    <telerik:AjaxUpdatedControl ControlID="pnlPatientInfo" />
                    <telerik:AjaxUpdatedControl ControlID="hfServiceUnitID" />
                    <telerik:AjaxUpdatedControl ControlID="hfParamedicID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCancel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlContent" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMessage" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMain" />
                    <telerik:AjaxUpdatedControl ControlID="listPoli" />
                    <telerik:AjaxUpdatedControl ControlID="listQueue" />
                    <telerik:AjaxUpdatedControl ControlID="GridRegistered" />
                    <telerik:AjaxUpdatedControl ControlID="listDokter" />
                    <telerik:AjaxUpdatedControl ControlID="MultipleAppt" />
                    <telerik:AjaxUpdatedControl ControlID="pnlPatientInfo" />
                    <telerik:AjaxUpdatedControl ControlID="hfServiceUnitID" />
                    <telerik:AjaxUpdatedControl ControlID="hfParamedicID" />
                    <telerik:AjaxUpdatedControl ControlID="btnProcess" />
                    <telerik:AjaxUpdatedControl ControlID="btnCancel" />
                    <telerik:AjaxUpdatedControl ControlID="txtMedicalNo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnProcess">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlContent" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMessage" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMain" />
                    <telerik:AjaxUpdatedControl ControlID="listPoli" />
                    <telerik:AjaxUpdatedControl ControlID="listQueue" />
                    <telerik:AjaxUpdatedControl ControlID="GridRegistered" />
                    <telerik:AjaxUpdatedControl ControlID="listDokter" />
                    <telerik:AjaxUpdatedControl ControlID="MultipleAppt" />
                    <telerik:AjaxUpdatedControl ControlID="pnlPatientInfo" />
                    <telerik:AjaxUpdatedControl ControlID="hfServiceUnitID" />
                    <telerik:AjaxUpdatedControl ControlID="hfParamedicID" />
                    <telerik:AjaxUpdatedControl ControlID="btnProcess" />
                    <telerik:AjaxUpdatedControl ControlID="btnCancel" />
                    <telerik:AjaxUpdatedControl ControlID="txtMedicalNo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="GridRegistered">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlContent" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMessage" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMain" />
                    <telerik:AjaxUpdatedControl ControlID="GridRegistered" />
                    <telerik:AjaxUpdatedControl ControlID="listPoli" />
                    <telerik:AjaxUpdatedControl ControlID="listQueue" />
                    <telerik:AjaxUpdatedControl ControlID="listDokter" />
                    <telerik:AjaxUpdatedControl ControlID="MultipleAppt" />
                    <telerik:AjaxUpdatedControl ControlID="pnlPatientInfo" />
                    <telerik:AjaxUpdatedControl ControlID="hfServiceUnitID" />
                    <telerik:AjaxUpdatedControl ControlID="hfParamedicID" />
                    <telerik:AjaxUpdatedControl ControlID="btnProcess" />
                    <telerik:AjaxUpdatedControl ControlID="btnCancel" />
                    <telerik:AjaxUpdatedControl ControlID="txtMedicalNo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="listQueue">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlContent" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMessage" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMain" />
                    <telerik:AjaxUpdatedControl ControlID="listPoli" />
                    <telerik:AjaxUpdatedControl ControlID="listQueue" />
                    <telerik:AjaxUpdatedControl ControlID="GridRegistered" />
                    <telerik:AjaxUpdatedControl ControlID="listDokter" />
                    <telerik:AjaxUpdatedControl ControlID="MultipleAppt" />
                    <telerik:AjaxUpdatedControl ControlID="pnlPatientInfo" />
                    <telerik:AjaxUpdatedControl ControlID="hfServiceUnitID" />
                    <telerik:AjaxUpdatedControl ControlID="hfParamedicID" />
                    <telerik:AjaxUpdatedControl ControlID="btnProcess" />
                    <telerik:AjaxUpdatedControl ControlID="btnCancel" />
                    <telerik:AjaxUpdatedControl ControlID="txtMedicalNo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Outlook" />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" BorderColor="#000066" Height="700px"
        BorderStyle="Solid" BorderWidth="1px" Font-Size="X-Large" CssClass="contentkiosk">
        <telerik:RadSplitter ID="MainSplitter" runat="server" Orientation="Vertical" Width="100%"
            Height="700px">
            <telerik:RadPane ID="CalendarPane" runat="server" Width="190px" Scrolling="None">
                <telerik:RadToolBar ID="CalendarToolbar" runat="server" Width="100%">
                    <Items>
                        <telerik:RadToolBarButton Text="Lang / Bahasa" Font-Bold="true" Font-Size="16px"
                            Font-Names="arial" Width="100%" />
                    </Items>
                </telerik:RadToolBar>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnLangIna" runat="server" Text="Indonesia" CssClass="skip btnLang"
                                CommandArgument="ina" OnClick="btn_Click" Font-Size="X-Large" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnLangEn" runat="server" Text="English" CssClass="skip btnLang"
                                CommandArgument="en" OnClick="btn_Click" Font-Size="X-Large" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btn_Click" CssClass="skip btnLang" Font-Size="X-Large" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnQueue" runat="server" Text="Clear" OnClick="btn_Click" CssClass="skip btnLang" Font-Size="X-Large" Visible="false" />
                        </td>
                    </tr>
                </table>
            </telerik:RadPane>
            <telerik:RadPane ID="NestedSplitterPane" runat="server" Scrolling="None">
                <telerik:RadSplitter ID="NestedSplitter" runat="server" Orientation="Horizontal">
                    <telerik:RadPane ID="rpEntry" runat="server" Scrolling="None">
                        <telerik:RadToolBar ID="rtbEntry" runat="server" Width="100%">
                            <Items>
                                <telerik:RadToolBarButton Text="Pendaftaran Mandiri" Font-Bold="true" Font-Size="16px"
                                    Font-Names="arial" Width="100%" />
                            </Items>
                        </telerik:RadToolBar>
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCommand" runat="server" Text="Silahkan Memasukkan Nomor Rekam Medis"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td valign="top" style="width: 315px;">
                                                <table width="100%">
                                                    <tr>
                                                        <td class="entry">
                                                            <table id="tblKeypad" runat="server">
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Font-Size="XX-Large" Font-Bold="True" Width="300px">
                                                                        </telerik:RadTextBox>
                                                                        <asp:HiddenField ID="hfServiceUnitID" runat="server" />
                                                                        <asp:HiddenField ID="hfParamedicID" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btn1" runat="server" Text="1" OnClientClick="NumberPush(this); return false;" CssClass="punch btnNo" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btn2" runat="server" Text="2" OnClientClick="NumberPush(this); return false;" CssClass="punch btnNo" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btn3" runat="server" Text="3" OnClientClick="NumberPush(this); return false;" CssClass="punch btnNo" />
                                                                    </td>                                                                   
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btn4" runat="server" Text="4" OnClientClick="NumberPush(this); return false;" CssClass="punch btnNo" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btn5" runat="server" Text="5" OnClientClick="NumberPush(this); return false;" CssClass="punch btnNo" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btn6" runat="server" Text="6" OnClientClick="NumberPush(this); return false;" CssClass="punch btnNo" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btn7" runat="server" Text="7" OnClientClick="NumberPush(this); return false;" CssClass="punch btnNo" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btn8" runat="server" Text="8" OnClientClick="NumberPush(this); return false;" CssClass="punch btnNo" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btn9" runat="server" Text="9" OnClientClick="NumberPush(this); return false;" CssClass="punch btnNo" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btn0" runat="server" Text="0" OnClientClick="NumberPush(this); return false;" CssClass="punch btnNo" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnBackspace" runat="server" Text="Erase" OnClick="btnBackspace_Click" CssClass="punch-red btnNo" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnOk" runat="server" Text="OK" OnClick="btnOk_Click" CssClass="punch-green btnNo" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            
                                                            <telerik:RadListView runat="server" ID="listQueue" ItemPlaceholderID="itemPlaceholder"
                                                                OnItemCommand="listQueue_ItemCommand" Width="100%" 
                                                                DataKeyNames="ItemID" >
                                                                <LayoutTemplate>
                                                                        <div id="itemPlaceholder" runat="server"></div>
                                                                </LayoutTemplate>
                                                                <EmptyDataTemplate>
                                                                    No records.
                                                                </EmptyDataTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <div>
                                                                        <asp:Button runat="server" ID="EditButton" CommandName="Pick"  
                                                                            style='<%#string.Format("width:310px; height:60px; font-size:{0}px;",
                                                                            Eval("ItemName").ToString().Length > 9 ? "18":"24" )%>'
                                                                            CssClass='<%#(true).Equals(true) ? "minimal1" : "minimal2" %>'
                                                                            Text='<%#Eval("ItemName") %>' Font-Size="X-Large" />
                                                                        <hr />
                                                                    </div>
                                                                </AlternatingItemTemplate>
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:Button runat="server" ID="EditButton" CommandName="Pick"  
                                                                            style='<%#string.Format("width:310px; height:60px; font-size:{0}px;",
                                                                            Eval("ItemName").ToString().Length > 9 ? "18":"24" )%>'
                                                                            CssClass='<%#(true).Equals(true) ? "minimal1" : "minimal2" %>'
                                                                            Text='<%#Eval("ItemName") %>' Font-Size="X-Large" />
                                                                        <hr />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </telerik:RadListView>
                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top">
                                                <asp:Panel ID="pnlContent" runat="server" Width="100%" Height="400px">
                                                    <asp:Panel ID="pnlMessage" runat="server" Width="100%" Font-Bold="false" Font-Size="Large">
                                                        <telerik:RadToolBar ID="rtbMessage" runat="server" Width="100%">
                                                            <Items>
                                                                <telerik:RadToolBarButton Text="Informasi" Font-Bold="true" Font-Size="16px" Font-Names="arial"
                                                                    Width="100%" />
                                                            </Items>
                                                        </telerik:RadToolBar>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblUserMessage" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <telerik:RadGrid ID="GridRegistered" runat="server" AutoGenerateColumns="false" 
                                                    OnNeedDataSource="GridRegistered_NeedDataSource">
                                                        <MasterTableView DataKeyNames="RegistrationNo">
                                                            <Columns>
                                                                <telerik:GridTemplateColumn>
                                                                    <ItemTemplate>
                                                                        <asp:Button runat="server" ID="btnPrint" CommandName="Print" CssClass="shiny-blue"
                                                                        Text='<%#LangToID(Eval("ButtonText").ToString()) %>' OnClick="btn_Click" CommandArgument='<%#Eval("RegistrationNo") %>'  />
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn> 
                                                                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="RegistrationNo" HeaderText="Registration No"
                                                                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" />
                                                                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name"
                                                                    UniqueName="PatientName" SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" />
                                                                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Room"
                                                                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" />
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                    <telerik:RadListView runat="server" ID="listPoli" ItemPlaceholderID="itemPlaceholder"
                                                        OnItemCommand="listPoli_ItemCommand">
                                                        <LayoutTemplate>
                                                            <telerik:RadToolBar ID="rtbListPoli" runat="server" Width="100%">
                                                                <Items>
                                                                    <telerik:RadToolBarButton Text="Poliklinik" Font-Bold="true" Font-Size="16px" Font-Names="arial"
                                                                        Width="100%" />
                                                                </Items>
                                                            </telerik:RadToolBar>
                                                            <div class="scrollx" style="text-align:center">
                                                                <div id="itemPlaceholder" runat="server"></div>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <EmptyDataTemplate>
                                                            No records.
                                                        </EmptyDataTemplate>
                                                        <AlternatingItemTemplate>
                                                                    <asp:Button runat="server" ID="EditButton" CommandName="Pick" 
                                                                        style='<%#string.Format("width:150px; height:60px; font-size:{0}px; float:left;",
                                                                        Eval("ServiceUnitName").ToString().Length > 9 ? "18":"24" )%>'
                                                                        CssClass='<%#((bool)Eval("ApptHasScheduleToDay")).Equals(true) ? "minimal1" : "minimal2" %>'
                                                                        Text='<%#Eval("ServiceUnitName") %>' Font-Size="X-Large" />
                                                        </AlternatingItemTemplate>
                                                        <ItemTemplate>
                                                                    <asp:Button runat="server" ID="EditButton" CommandName="Pick"  
                                                                        style='<%#string.Format("width:150px; height:60px; font-size:{0}px; float:left;",
                                                                        Eval("ServiceUnitName").ToString().Length > 9 ? "18":"24" )%>'
                                                                        CssClass='<%#((bool)Eval("ApptHasScheduleToDay")).Equals(true) ? "minimal1" : "minimal2" %>'
                                                                        Text='<%#Eval("ServiceUnitName") %>' Font-Size="X-Large" />
                                                        </ItemTemplate>
                                                    </telerik:RadListView>
                                                    <telerik:RadListView runat="server" ID="listDokter" ItemPlaceholderID="itemPlaceholder2"
                                                        OnItemCommand="listDokter_ItemCommand" OnItemDataBound="listDokter_ItemDataBound">
                                                        <LayoutTemplate>
                                                            <telerik:RadToolBar ID="rtblistDokter" runat="server" Width="100%">
                                                                <Items>
                                                                    <telerik:RadToolBarButton Text="Dokter" Font-Bold="true" Font-Size="16px" Font-Names="arial"
                                                                        Width="100%" />
                                                                </Items>
                                                            </telerik:RadToolBar>
                                                            <div class="scroll">
                                                                <table style="background-color: #D9DFDF;">
                                                                    <tr runat="server" id="itemPlaceholder2" />
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <EmptyDataTemplate>
                                                            No records.
                                                        </EmptyDataTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr id="Tr2" runat="server" bgcolor="#d3d3d3">
                                                                <td align="center">
                                                                    <asp:Button runat="server" ID="EditButton" CommandName="Pick" CssClass="minimal"
                                                                        Text='<%#Eval("ParamedicName") %>' Font-Size="Large" />
                                                                </td>
                                                                <td>
                                                                    <span class="schinfo"><%#Eval("Notes") %></span>
                                                                </td>
                                                                <td>
                                                                    <asp:Image runat="server" ID="iFoto" Height="100" />
                                                                </td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                        <ItemTemplate>
                                                            <tr id="Tr2" runat="server" bgcolor="#e3e3e3">
                                                                <td align="center">
                                                                    <asp:Button runat="server" ID="EditButton" CommandName="Pick" CssClass="minimal"
                                                                        Text='<%#Eval("ParamedicName") %>' Font-Size="Large" />
                                                                </td>
                                                                <td>
                                                                    <span class="schinfo"><%#Eval("Notes") %></span>
                                                                </td>
                                                                <td>
                                                                    <asp:Image runat="server" ID="iFoto" Height="100" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </telerik:RadListView>
                                                    <telerik:RadListView runat="server" ID="listMultipleAppt" ItemPlaceholderID="itemPlaceholder"
                                                        OnItemCommand="listMultipleAppt_ItemCommand">
                                                        <LayoutTemplate>
                                                            <table style="background-color: #D9DFDF;">
                                                                <tr>
                                                                    <th>
                                                                    </th>
                                                                    <th id="Th1" runat="server">
                                                                        Service Unit Name
                                                                    </th>
                                                                    <th id="Th2" runat="server">
                                                                        Physician
                                                                    </th>
                                                                </tr>
                                                                <tr runat="server" id="itemPlaceholder" />
                                                            </table>
                                                        </LayoutTemplate>
                                                        <EmptyDataTemplate>
                                                            <legend>Appointment</legend>No records.
                                                        </EmptyDataTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr id="Tr2" runat="server" bgcolor="#ccffff">
                                                                <td>
                                                                    <asp:Button runat="server" ID="EditButton" CommandName="Pick" CssClass="shiny-blue"
                                                                        Text='<%#LangToID("Pick") %>' Font-Size="X-Large" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="CustomerID" runat="Server" Text='<%#Eval("ServiceUnitName") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="CompanyName" runat="Server" Text='<%#Eval("ParamedicName") %>' />
                                                                </td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                        <ItemTemplate>
                                                            <tr id="Tr2" runat="server" bgcolor="#99ccff">
                                                                <td>
                                                                    <asp:Button runat="server" ID="EditButton" CommandName="Pick" CssClass="shiny-blue"
                                                                        Text='<%#LangToID("Pick") %>' Font-Size="X-Large" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="CustomerID" runat="Server" Text='<%#Eval("ServiceUnitName") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="CompanyName" runat="Server" Text='<%#Eval("ParamedicName") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </telerik:RadListView>
                                                    <asp:Panel ID="pnlPatientInfo" runat="server" Width="100%" Font-Bold="false" Font-Size="X-Large">
                                                        <telerik:RadToolBar ID="rtbPatientInfo" runat="server" Width="100%">
                                                            <Items>
                                                                <telerik:RadToolBarButton Text="Informasi Pasien" Font-Bold="true" Font-Size="16px"
                                                                    Font-Names="arial" Width="100%" />
                                                            </Items>
                                                        </telerik:RadToolBar>
                                                        <table>
                                                            <tr>
                                                                <td class="label">
                                                                    <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPMedicalNo" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="label">
                                                                    <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPPatientName" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="label">
                                                                    <asp:Label ID="lblDateOfBirth" runat="server" Text="Date Of Birth"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPDateOfBirth" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="label">
                                                                    <asp:Label ID="lblAppointmentNo" runat="server" Text="Appointment No"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPAppointmentNo" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="label">
                                                                    <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPServiceUnit" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="label">
                                                                    <asp:Label ID="lblPhysician" runat="server" Text="Phisician"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPPhysician" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="label">
                                                                    <asp:Label ID="lblQueNo" runat="server" Text="Que No"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPQueNo" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td><asp:Button ID="btnProcess" runat="server" Text="Process" CssClass="punch-green btnRegister"
                                                                            OnClick="btnProcess_Click" /></td>
                                                                            <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="punch-red btnRegister"
                                                                            OnClick="btn_Click" />  </td>
                                                                        </tr>
                                                                    </table>  
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlMain" runat="server" Width="100%" Font-Bold="false" Font-Size="Large">
                                                        <img src="../../../../Images/LogoRS_MainMenu.jpg" width="100%" />
                                                    </asp:Panel>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>
</asp:Content>
