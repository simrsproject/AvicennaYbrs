<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InternalPe2Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.InternalPe2Ctl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/QuestionCtl.ascx" TagPrefix="uc1" TagName="QuestionCtl" %>

<table style="width: 100%">
    <tr>
        <td valign="top" style="width: 50%;">
            <fieldset style="width: 98%;">
                <legend>PHYSICAL EXAMINATION</legend>
                <uc1:GcsCtl runat="server" ID="gcsCtl" />
                <table width="100%">
                    <tr>
                        <td class="label">Head</td>
                        <td style="width: 150px">
                            <asp:RadioButtonList ID="optKepala" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtKepala" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Eyes</td>
                        <td>
                            <asp:RadioButtonList ID="optMata" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMata" runat="server" Width="100%" />
                        </td>
                        <td></td>

                    </tr>
                    <tr>
                        <td class="label">ENT</td>
                        <td>
                            <asp:RadioButtonList ID="optTht" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTht" runat="server" Width="100%" />
                        </td>
                        <td></td>

                    </tr>
                    <tr>
                        <td class="label">Mouth</td>
                        <td>
                            <asp:RadioButtonList ID="optMulut" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMulut" runat="server" Width="100%" />
                        </td>
                        <td></td>

                    </tr>
                    <tr>
                        <td class="label">Neck</td>
                        <td>
                            <asp:RadioButtonList ID="optLeher" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtLeher" runat="server" Width="100%" />
                        </td>
                        <td></td>

                    </tr>
                    <tr>
                        <td class="label">Thorax</td>
                        <td>
                            <asp:RadioButtonList ID="optThorax" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtThorax" runat="server" Width="100%" />
                        </td>
                        <td></td>

                    </tr>

                    <tr>
                        <td class="label">Heart</td>
                        <td>
                            <asp:RadioButtonList ID="optJantung" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtJantung" runat="server" Width="100%" />
                        </td>
                        <td></td>

                    </tr>
                    <tr>
                        <td class="label">Lungs</td>
                        <td>
                            <asp:RadioButtonList ID="optParu" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParu" runat="server" Width="100%" />
                        </td>
                        <td></td>

                    </tr>
                    <tr>
                        <td class="label">Abdomen</td>
                        <td>
                            <asp:RadioButtonList ID="optAbdomen" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAbdomen" runat="server" Width="100%" />
                        </td>
                        <td></td>

                    </tr>

                    <tr>
                        <td class="label">Auskulatasi</td>
                        <td>
                            <asp:RadioButtonList ID="optAuskulatasi" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAuskulatasi" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Genitalia & Anus</td>
                        <td>
                            <asp:RadioButtonList ID="optGenitaliaAndAnus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtGenitaliaAndAnus" runat="server" Width="100%" />
                        </td>
                        <td></td>

                    </tr>
                    <tr>
                        <td class="label">Extremity</td>
                        <td>
                            <asp:RadioButtonList ID="optEkstremitas" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEkstremitas" runat="server" Width="100%" />
                        </td>
                        <td></td>

                    </tr>
                    <tr>
                        <td class="label">Skin</td>
                        <td>
                            <asp:RadioButtonList ID="optKulit" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtKulit" runat="server" Width="100%" />
                        </td>
                        <td></td>

                    </tr>
                    <tr>
                        <td colspan="3" class="labelcaption">&nbsp;</td>
                    </tr>
<%--                    <tr>
                        <td class="label">Ancillary Examination</td>
                        <td colspan="2">
                            <telerik:RadTextBox ID="txtOtherExam" runat="server" Width="100%" Height="80px" TextMode="MultiLine" Resize="Vertical" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="label">Notes
                        </td>
                        <td class="entry" colspan="2">
                            <telerik:RadTextBox ID="txtPhysicalExamNotes" runat="server" Width="100%" Height="80px"
                                TextMode="MultiLine" Resize="Vertical" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </fieldset>

        </td>
    </tr>

</table>
