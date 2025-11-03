<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GeneralV2Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.GeneralV2Ctl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<table style="width: 100%; padding: 0 0 0 0;">
    <tr>
        <td style="width: 50%;" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="Label6" runat="server" Text="Anamnesis Type"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDropDownList ID="cboAnamnesisType" runat="server" Width="150px">
                            <Items>
                                <telerik:DropDownListItem Text="Autoanamnesis" Value="Auto" Selected="true"></telerik:DropDownListItem>
                                <telerik:DropDownListItem Text="Alloanamnesis, from" Value="Allo"></telerik:DropDownListItem>
                            </Items>
                        </telerik:RadDropDownList>
                        <telerik:RadTextBox ID="txtAlloanamnesisSource" runat="server" Width="150px" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label2" runat="server" Text="Chief Complaint"></asp:Label>
                                                    <asp:LinkButton runat="server" ID="lbtnPrevComplaint" OnClick="lbtnPrevComplaint_OnClick" OnClientClick="if (!confirm('Copy Chief Complaint from previouse OutPatient Assessment?')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                            </asp:LinkButton>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtComplaint" runat="server" Width="100%" Height="40px" Resize="Vertical"
                            TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="History of Present Illness"></asp:Label>
                <asp:LinkButton runat="server" ID="lbtnPrevHpi" OnClick="lbtnPrevHpi_OnClick" OnClientClick="if (!confirm('Copy History of Present Illness previouse OutPatient')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtHpi" runat="server" Width="100%" Height="60px" Resize="Vertical"
                            TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label4" runat="server" Text="Other"></asp:Label>
                        <asp:LinkButton runat="server" ID="lbtnPrevAnamnesisNotes" OnClick="lbtnPrevAnamnesisNotes_OnClick" OnClientClick="if (!confirm('Copy other anamnesis from previouse Assessment?')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                            </asp:LinkButton>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtAnamnesisNotes" runat="server" Width="100%" Height="60px" Resize="Vertical"
                            TextMode="MultiLine" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top;">
            <fieldset>
                <legend>VITAL SIGN</legend>
                <telerik:RadGrid ID="grdVitalSign" runat="server" 
                    AutoGenerateColumns="False" GridLines="None">
                    <MasterTableView DataKeyNames="VitalSignID" ShowHeader="False">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Question">
                                <ItemTemplate>
                                    <a href="javascript:void(0);" onclick='<%# string.Format("javascript:openVitalSignChart(\"{0}\")", DataBinder.Eval(Container.DataItem, "VitalSignID")) %>'>
                                        <%#DataBinder.Eval(Container.DataItem, "VitalSignName")%>
                                    </a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridDateTimeColumn DataField="RecordDate" UniqueName="RecordDate" HeaderText="Date" HeaderStyle-Width="80px"></telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="RecordTime" UniqueName="RecordTime" HeaderText="Time" HeaderStyle-Width="50px"></telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Answer" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <span>
                                        <%# string.Format("<div style='background-color: {0};width:100%;padding-left: 2px'>{1}</div>",DataBinder.Eval(Container.DataItem, "EwsLevelColor"),DataBinder.Eval(Container.DataItem, "QuestionAnswerFormatted"))%>
                                    </span>                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="">
                                <HeaderStyle Width="30px"></HeaderStyle>
                                <ItemTemplate>
                                    <a href="javascript:void(0);" onclick='<%# string.Format("javascript:openVitalSignChart(\"{0}\")", DataBinder.Eval(Container.DataItem, "VitalSignID")) %>'>
                                        <img src='../../../../../Images/Toolbar/barchart.bmp' alt='chart' /></a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="false">
                        <Selecting AllowRowSelect="false" />
                        <Resizing AllowColumnResize="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </fieldset>
            <telerik:RadAjaxPanel runat="server" id="ajxPnlFdolm">
            <table width="100%">
            <tr>
                <td class="label">First day of last menstruation</td>
                <td style="width: 150px">
                    <telerik:RadDatePicker ID="txtFdolm" runat="server" Width="150px" AutoPostBack="true" OnSelectedDateChanged="txtFdolm_SelectedDateChanged">
                    </telerik:RadDatePicker>
                </td>
                <td class="entry"> </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Est Birth Date</td>
                <td style="width: 150px">
                    <telerik:RadDatePicker ID="txtEstBirthDate" runat="server" Width="123px" DatePopupButton-Visible="false" DateInput-ReadOnly="true">
                    </telerik:RadDatePicker>
                </td>
                <td class="entry"><asp:Label runat="server" id="lblPregnantAge"></asp:Label></td>
                <td></td>
            </tr>
            </table>
            </telerik:RadAjaxPanel>
        </td>
    </tr>
</table>
