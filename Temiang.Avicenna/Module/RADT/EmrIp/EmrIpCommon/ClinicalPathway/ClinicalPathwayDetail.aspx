<%@  Title="Clinical Pathway" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="ClinicalPathwayDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.ClinicalPathwayDetail" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript" language="javascript">
            function realized(pwid, seqno, dayNo, pwitid, pwitname) {
                if (pwitid == '') {
                    __doPostBack("<%=grdList.UniqueID %>", "realized_" + seqno + "_" + dayNo);
                }
                else {
                    var url = "SelectTransactionItem.aspx?pwid=" + pwid + "&seqno=" + seqno + "&dayNo=" + dayNo + "&pwitid=" + pwitid + "&pwitname=" + pwitname + "&regno=<%=RegistrationNo %>&regdate=<%=txtRegistrationDate.SelectedDate.Value.Date %>&ccm=rebind&cet=<%=grdList.ClientID %>";
                    openWindow(url, 600, 700);
                }
            }
            function unRealized(pathwayItemSeqNo, dayNo) {
                if (!confirm('UnRealized this Assessment ?')) return false;

                __doPostBack("<%=grdList.UniqueID %>", "unRealized_" + pathwayItemSeqNo + "_" + dayNo);
            }
            function openWindow(url, width, height) {
                var oWnd;
                oWnd = radopen(url, 'winDialog');
                oWnd.setSize(width, height);
                oWnd.center();
            }
            function radWindowManager_ClientClose(oWnd, args) {
                //get the transferred arguments from popup
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
    <telerik:RadAjaxManagerProxy runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtPathwayID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPathwayName" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboPathwayStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboPathwayStatus" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="txtPathwayID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="radWindowManager_ClientClose">
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Pathway ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPathwayID" runat="server" Width="300px" MaxLength="20"
                                OnTextChanged="txtPathwayID_TextChanged" AutoPostBack="True" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Pathway Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPathwayName" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Registration Date Time
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtRegistrationTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px" ReadOnly="True">
                                        </telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Medical No
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="275px" MaxLength="20"
                                ReadOnly="true" />
                            <telerik:RadTextBox ID="txtGender" runat="server" Width="22px" ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <telerik:RadNumericTextBox ID="txtAgeInYear" runat="server" Width="30px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Y&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeInMonth" runat="server" Width="30px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;M&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeInDay" runat="server" Width="30px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;D
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
                        <td width="20" />
                        <td />
                    </tr>

                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
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
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Service Unit
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
                        <td width="20" />
                        <td />
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
                        <td width="20" />
                        <td />
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
                            <asp:Label ID="lblBedID" runat="server" Text="Bed"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBedID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr runat="server" id="pnlLengthOfStay">
                        <td class="label">
                            <asp:Label ID="lblLengthOfStay" runat="server" Text="Length Of Stay"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <telerik:RadNumericTextBox ID="txtLengthOfStay" runat="server" Width="50px" ReadOnly="True">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Day(s)
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Diagnose"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtDiagnoseID" runat="server" Width="100px" MaxLength="20"
                                            ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDiagnoseName" runat="server" CssClass="labeldescription" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Pathway Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDropDownList ID="cboPathwayStatus" runat="server" Width="300px" OnSelectedIndexChanged="cboPathwayStatus_OnSelectedIndexChanged" AutoPostBack="True">
                                <Items>
                                    <telerik:DropDownListItem Text="" Value=""></telerik:DropDownListItem>
                                    <telerik:DropDownListItem Text="Accept" Value="A"></telerik:DropDownListItem>
                                    <telerik:DropDownListItem Text="Fail" Value="F"></telerik:DropDownListItem>
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Notes
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtNotes" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server"
        AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="PathwayID, PathwayItemSeqNo" CommandItemDisplay="None">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="AssesmentHeaderName" HeaderText="Header Name "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="AssesmentHeaderName" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="AssesmentGroupName" HeaderText="Group Name " />
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="AssesmentGroupName" SortOrder="Ascending" />
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridBoundColumn DataField="AssesmentName" HeaderText="Assessment Name" UniqueName="AssesmentName"
                    SortExpression="AssesmentName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="400px" />
                <telerik:GridTemplateColumn HeaderStyle-Width="250px" HeaderText="Day 1" UniqueName="chk_1"
                    SortExpression="chk_1" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <table style="width: 100%; <%# "01".Equals(DataBinder.Eval(Container.DataItem, "col_1").ToString()) || "02".Equals(DataBinder.Eval(Container.DataItem, "col_1").ToString())  ? "": "display:none" %>">
                            <tr>
                                <td style="background: <%# "01".Equals(DataBinder.Eval(Container.DataItem, "col_1")) ? Color01:Color02 %>">
                                    <%# (false.Equals(DataBinder.Eval(Container.DataItem, "chk_1")) ? 
                                            string.Format("<a href=\"#\" onclick=\"javascript:realized('{1}',{2}, 1,'{3}','{4}'); return false;\"><img src=\"{0}/Images/Toolbar/post16.png\"  alt=\"Realized\" /></a>",Helper.UrlRoot(), txtPathwayID.Text, DataBinder.Eval(Container.DataItem, "PathwayItemSeqNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "AssesmentName").ToString().Replace("'","_"))
                                            :string.Format("<img src=\"{0}/Images/Toolbar/post16_d.png\"  alt=\"view\" />",Helper.UrlRoot())) %>                                   
                                    &nbsp;&nbsp;
                                    <%# (true.Equals(DataBinder.Eval(Container.DataItem, "chk_1")) ? 
                                            string.Format("<a href=\"#\" onclick=\"javascript:unRealized('{1}','1'); return false;\"><img src=\"{0}/Images/Toolbar/cancel16.png\"  alt=\"Realized\" /></a>",Helper.UrlRoot(),DataBinder.Eval(Container.DataItem, "PathwayItemSeqNo"))
                                            :string.Format("<img src=\"{0}/Images/Toolbar/cancel16_d.png\"  alt=\"view\" />",Helper.UrlRoot())) %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%; <%# true.Equals(DataBinder.Eval(Container.DataItem, "chk_1"))  ? "": "display:none" %>">
                                        <tr>
                                            <td style="width: 50px">Realized</td>
                                            <td style="width: 10px">:</td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chk_01" Text="" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "chk_1") %>' /></td>
                                        </tr>
                                        <tr>
                                            <td>Tx No</td>
                                            <td>:</td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "RefNo_1") %></td>
                                        </tr>
                                        <tr>
                                            <td>Intervention</td>
                                            <td>:</td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "InterventionItemName_1")%></td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="250px" HeaderText="Day 2" UniqueName="chk_2"
                    SortExpression="chk_2" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>

                        <table style="width: 100%; <%# "01".Equals(DataBinder.Eval(Container.DataItem, "col_2").ToString()) || "02".Equals(DataBinder.Eval(Container.DataItem, "col_2").ToString())  ? "": "display:none" %>">
                            <tr>
                                <td style="background: <%# "01".Equals(DataBinder.Eval(Container.DataItem, "col_2")) ? Color01:Color02 %>">
                                    <%# false.Equals(DataBinder.Eval(Container.DataItem, "chk_2")) ? 
                                            string.Format("<a href=\"#\" onclick=\"javascript:realized('{1}',{2}, 2,'{3}','{4}'); return false;\"><img src=\"{0}/Images/Toolbar/post16.png\"  alt=\"Realized\" /></a>",Helper.UrlRoot(), txtPathwayID.Text, DataBinder.Eval(Container.DataItem, "PathwayItemSeqNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "AssesmentName").ToString().Replace("'","_"))
                                            :string.Format("<img src=\"{0}/Images/Toolbar/post16_d.png\"  alt=\"view\" />",Helper.UrlRoot()) %>                                   
                                    &nbsp;&nbsp;
                                    <%# true.Equals(DataBinder.Eval(Container.DataItem, "chk_2")) ? 
                                            string.Format("<a href=\"#\" onclick=\"javascript:unRealized('{1}','2'); return false;\"><img src=\"{0}/Images/Toolbar/cancel16.png\"  alt=\"Realized\" /></a>",Helper.UrlRoot(),DataBinder.Eval(Container.DataItem, "PathwayItemSeqNo"))
                                            :string.Format("<img src=\"{0}/Images/Toolbar/cancel16_d.png\"  alt=\"view\" />",Helper.UrlRoot()) %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%; <%# true.Equals(DataBinder.Eval(Container.DataItem, "chk_2"))  ? "": "display:none" %>">
                                        <tr>
                                            <td style="width: 50px">Realized</td>
                                            <td style="width: 10px">:</td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chk_02" Text="" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "chk_2") %>' /></td>
                                        </tr>
                                        <tr>
                                            <td>Tx No</td>
                                            <td>:</td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "RefNo_2") %></td>
                                        </tr>
                                        <tr>
                                            <td>Intervention</td>
                                            <td>:</td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "InterventionItemName_2")%></td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="250px" HeaderText="Day 3" UniqueName="chk_3"
                    SortExpression="chk_3" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <table style="width: 100%; <%# "01".Equals(DataBinder.Eval(Container.DataItem, "col_3").ToString()) || "02".Equals(DataBinder.Eval(Container.DataItem, "col_3").ToString())  ? "": "display:none" %>">
                            <tr>
                                <td style="background: <%# "01".Equals(DataBinder.Eval(Container.DataItem, "col_2")) ? Color01:Color02 %>">
                                    <%# false.Equals(DataBinder.Eval(Container.DataItem, "chk_3")) ? 
                                            string.Format("<a href=\"#\" onclick=\"javascript:realized('{1}',{2}, 3,'{3}','{4}'); return false;\"><img src=\"{0}/Images/Toolbar/post16.png\"  alt=\"Realized\" /></a>",Helper.UrlRoot(), txtPathwayID.Text, DataBinder.Eval(Container.DataItem, "PathwayItemSeqNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "AssesmentName").ToString().Replace("'","_"))
                                            :string.Format("<img src=\"{0}/Images/Toolbar/post16_d.png\"  alt=\"view\" />",Helper.UrlRoot()) %>                                   
                                    &nbsp;&nbsp;
                                    <%# true.Equals(DataBinder.Eval(Container.DataItem, "chk_3")) ? 
                                            string.Format("<a href=\"#\" onclick=\"javascript:unRealized('{1}','3'); return false;\"><img src=\"{0}/Images/Toolbar/cancel16.png\"  alt=\"Realized\" /></a>",Helper.UrlRoot(),DataBinder.Eval(Container.DataItem, "PathwayItemSeqNo"))
                                            :string.Format("<img src=\"{0}/Images/Toolbar/cancel16_d.png\"  alt=\"view\" />",Helper.UrlRoot()) %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%; <%# true.Equals(DataBinder.Eval(Container.DataItem, "chk_3"))  ? "": "display:none" %>">
                                        <tr>
                                            <td style="width: 50px">Realized</td>
                                            <td style="width: 10px">:</td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chk_03" Text="" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "chk_3") %>' /></td>
                                        </tr>
                                        <tr>
                                            <td>Tx No</td>
                                            <td>:</td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "RefNo_3") %></td>
                                        </tr>
                                        <tr>
                                            <td>Intervention</td>
                                            <td>:</td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "InterventionItemName_3")%></td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                        </table>

                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="250px" HeaderText="Day 4" UniqueName="chk_4"
                    SortExpression="chk_4" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <table style="width: 100%; <%# "01".Equals(DataBinder.Eval(Container.DataItem, "col_4").ToString()) || "02".Equals(DataBinder.Eval(Container.DataItem, "col_4").ToString())  ? "": "display:none" %>">
                            <tr>
                                <td style="background: <%# "01".Equals(DataBinder.Eval(Container.DataItem, "col_2")) ? Color01:Color02 %>">
                                    <%# false.Equals(DataBinder.Eval(Container.DataItem, "chk_4")) ? 
                                            string.Format("<a href=\"#\" onclick=\"javascript:realized('{1}',{2}, 4,'{3}','{4}'); return false;\"><img src=\"{0}/Images/Toolbar/post16.png\"  alt=\"Realized\" /></a>",Helper.UrlRoot(), txtPathwayID.Text, DataBinder.Eval(Container.DataItem, "PathwayItemSeqNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "AssesmentName").ToString().Replace("'","_"))
                                            :string.Format("<img src=\"{0}/Images/Toolbar/post16_d.png\"  alt=\"view\" />",Helper.UrlRoot()) %>                                   
                                    &nbsp;&nbsp;
                                    <%# true.Equals(DataBinder.Eval(Container.DataItem, "chk_4")) ? 
                                            string.Format("<a href=\"#\" onclick=\"javascript:unRealized('{1}','4'); return false;\"><img src=\"{0}/Images/Toolbar/cancel16.png\"  alt=\"Realized\" /></a>",Helper.UrlRoot(),DataBinder.Eval(Container.DataItem, "PathwayItemSeqNo"))
                                            :string.Format("<img src=\"{0}/Images/Toolbar/cancel16_d.png\"  alt=\"view\" />",Helper.UrlRoot()) %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%; <%# true.Equals(DataBinder.Eval(Container.DataItem, "chk_4"))  ? "": "display:none" %>">
                                        <tr>
                                            <td style="width: 50px">Realized</td>
                                            <td style="width: 10px">:</td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chk_04" Text="" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "chk_4") %>' /></td>
                                        </tr>
                                        <tr>
                                            <td>Tx No</td>
                                            <td>:</td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "RefNo_4") %></td>
                                        </tr>
                                        <tr>
                                            <td>Intervention</td>
                                            <td>:</td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "InterventionItemName_4")%></td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                        </table>

                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="250px" HeaderText="Day 5" UniqueName="chk_5"
                    SortExpression="chk_5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <table style="width: 100%; <%# "01".Equals(DataBinder.Eval(Container.DataItem, "col_5").ToString()) || "02".Equals(DataBinder.Eval(Container.DataItem, "col_5").ToString())  ? "": "display:none" %>">
                            <tr>
                                <td style="background: <%# "01".Equals(DataBinder.Eval(Container.DataItem, "col_2")) ? Color01:Color02 %>">
                                    <%# false.Equals(DataBinder.Eval(Container.DataItem, "chk_5")) ? 
                                            string.Format("<a href=\"#\" onclick=\"javascript:realized('{1}',{2}, 5,'{3}','{4}'); return false;\"><img src=\"{0}/Images/Toolbar/post16.png\"  alt=\"Realized\" /></a>",Helper.UrlRoot(), txtPathwayID.Text, DataBinder.Eval(Container.DataItem, "PathwayItemSeqNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "AssesmentName").ToString().Replace("'","_"))
                                            :string.Format("<img src=\"{0}/Images/Toolbar/post16_d.png\"  alt=\"view\" />",Helper.UrlRoot()) %>                                   
                                    &nbsp;&nbsp;
                                    <%# true.Equals(DataBinder.Eval(Container.DataItem, "chk_5")) ? 
                                            string.Format("<a href=\"#\" onclick=\"javascript:unRealized('{1}','5'); return false;\"><img src=\"{0}/Images/Toolbar/cancel16.png\"  alt=\"Realized\" /></a>",Helper.UrlRoot(),DataBinder.Eval(Container.DataItem, "PathwayItemSeqNo"))
                                            :string.Format("<img src=\"{0}/Images/Toolbar/cancel16_d.png\"  alt=\"view\" />",Helper.UrlRoot()) %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%; <%# true.Equals(DataBinder.Eval(Container.DataItem, "chk_5"))  ? "": "display:none" %>">
                                        <tr>
                                            <td style="width: 50px">Realized</td>
                                            <td style="width: 10px">:</td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chk_05" Text="" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "chk_5") %>' /></td>
                                        </tr>
                                        <tr>
                                            <td>Tx No</td>
                                            <td>:</td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "RefNo_5") %></td>
                                        </tr>
                                        <tr>
                                            <td>Intervention</td>
                                            <td>:</td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "InterventionItemName_5")%></td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="250px" HeaderText="Day 6" UniqueName="chk_6"
                    SortExpression="chk_6" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <table style="width: 100%; <%# "01".Equals(DataBinder.Eval(Container.DataItem, "col_6").ToString()) || "02".Equals(DataBinder.Eval(Container.DataItem, "col_6").ToString())  ? "": "display:none" %>">
                            <tr>
                                <td style="background: <%# "01".Equals(DataBinder.Eval(Container.DataItem, "col_2")) ? Color01:Color02 %>">
                                    <%# false.Equals(DataBinder.Eval(Container.DataItem, "chk_6")) ? 
                                            string.Format("<a href=\"#\" onclick=\"javascript:realized('{1}',{2}, 6,'{3}','{4}'); return false;\"><img src=\"{0}/Images/Toolbar/post16.png\"  alt=\"Realized\" /></a>",Helper.UrlRoot(), txtPathwayID.Text, DataBinder.Eval(Container.DataItem, "PathwayItemSeqNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "AssesmentName").ToString().Replace("'","_"))
                                            :string.Format("<img src=\"{0}/Images/Toolbar/post16_d.png\"  alt=\"view\" />",Helper.UrlRoot()) %>                                   
                                    &nbsp;&nbsp;
                                    <%# true.Equals(DataBinder.Eval(Container.DataItem, "chk_6")) ? 
                                            string.Format("<a href=\"#\" onclick=\"javascript:unRealized('{1}','6'); return false;\"><img src=\"{0}/Images/Toolbar/cancel16.png\"  alt=\"Realized\" /></a>",Helper.UrlRoot(),DataBinder.Eval(Container.DataItem, "PathwayItemSeqNo"))
                                            :string.Format("<img src=\"{0}/Images/Toolbar/cancel16_d.png\"  alt=\"view\" />",Helper.UrlRoot()) %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%; <%# true.Equals(DataBinder.Eval(Container.DataItem, "chk_6"))  ? "": "display:none" %>">
                                        <tr>
                                            <td style="width: 50px">Realized</td>
                                            <td style="width: 10px">:</td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chk_06" Text="" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "chk_6") %>' /></td>
                                        </tr>
                                        <tr>
                                            <td>Tx No</td>
                                            <td>:</td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "RefNo_6") %></td>
                                        </tr>
                                        <tr>
                                            <td>Intervention</td>
                                            <td>:</td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "InterventionItemName_6")%></td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="250px" HeaderText="Day 7" UniqueName="chk_7"
                    SortExpression="chk_7" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <table style="width: 100%; <%# "01".Equals(DataBinder.Eval(Container.DataItem, "col_7").ToString()) || "02".Equals(DataBinder.Eval(Container.DataItem, "col_7").ToString())  ? "": "display:none" %>">
                            <tr>
                                <td style="background: <%# "01".Equals(DataBinder.Eval(Container.DataItem, "col_2")) ? Color01:Color02 %>">
                                    <%# false.Equals(DataBinder.Eval(Container.DataItem, "chk_7")) ? 
                                            string.Format("<a href=\"#\" onclick=\"javascript:realized('{1}',{2}, 7,'{3}','{4}'); return false;\"><img src=\"{0}/Images/Toolbar/post16.png\"  alt=\"Realized\" /></a>",Helper.UrlRoot(), txtPathwayID.Text, DataBinder.Eval(Container.DataItem, "PathwayItemSeqNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "AssesmentName").ToString().Replace("'","_"))
                                            :string.Format("<img src=\"{0}/Images/Toolbar/post16_d.png\"  alt=\"view\" />",Helper.UrlRoot()) %>                                   
                                    &nbsp;&nbsp;
                                    <%# true.Equals(DataBinder.Eval(Container.DataItem, "chk_7")) ? 
                                            string.Format("<a href=\"#\" onclick=\"javascript:unRealized('{1}','7'); return false;\"><img src=\"{0}/Images/Toolbar/cancel16.png\"  alt=\"Realized\" /></a>",Helper.UrlRoot(),DataBinder.Eval(Container.DataItem, "PathwayItemSeqNo"))
                                            :string.Format("<img src=\"{0}/Images/Toolbar/cancel16_d.png\"  alt=\"view\" />",Helper.UrlRoot()) %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%; <%# true.Equals(DataBinder.Eval(Container.DataItem, "chk_7"))  ? "": "display:none" %>">
                                        <tr>
                                            <td style="width: 50px">Realized</td>
                                            <td style="width: 10px">:</td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chk_07" Text="" Enabled="false" Checked='<%# DataBinder.Eval(Container.DataItem, "chk_7") %>' /></td>
                                        </tr>
                                        <tr>
                                            <td>Tx No</td>
                                            <td>:</td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "RefNo_7") %></td>
                                        </tr>
                                        <tr>
                                            <td>Intervention</td>
                                            <td>:</td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "InterventionItemName_7")%></td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="PathwayID" UniqueName="PathwayID" SortExpression="PathwayID"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="PathwayItemSeqNo" UniqueName="PathwayItemSeqNo"
                    SortExpression="PathwayItemSeqNo" Visible="false" />
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="False" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
