<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InternalPeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.InternalPeCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/QuestionCtl.ascx" TagPrefix="uc1" TagName="QuestionCtl" %>

<table style="width: 100%">
    <tr>
        <td style="width: 50%">
            <fieldset style="width: 98%;">
                <legend>SYSTEM REVIEW</legend>
                <table style="width: 100%">
                    <tr>
                        <td valign="top">
                            <uc1:QuestionCtl runat="server" ID="revSysUmum" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysMata" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysTht" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysSistemPernafasan" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysSistemKardiovaskular" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysSistemPersyarafan" IsUseQuestionGroupCaption="True" />


                        </td>
                        <td valign="top">
                            <uc1:QuestionCtl runat="server" ID="revSysCardiovas" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysRespirasi" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysGastrointestinal" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysSaluranKencing" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysMuscle" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysHematologi" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysSistemEkskresi" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysSistemPencernaan" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysSistemMuskuloskeletal" IsUseQuestionGroupCaption="True" />

                        </td>
                        <td valign="top">
                            <uc1:QuestionCtl runat="server" ID="revSysEndokrin" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysDermatologi" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysNeurologi" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysPsikiatri" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysSistemReproduksi" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysDataPsikoSosiSpi" IsUseQuestionGroupCaption="True" />
                            <uc1:QuestionCtl runat="server" ID="revSysHambatanDiri" IsUseQuestionGroupCaption="True" />

                        </td>
                        <td></td>
                    </tr>
                </table>
            </fieldset>
        </td>
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
                    <tr>
                        <td class="label">Ancillary Examination</td>
                        <td colspan="2">
                            <telerik:RadTextBox ID="txtOtherExam" runat="server" Width="100%" Height="80px" TextMode="MultiLine" Resize="Vertical" />
                        </td>
                    </tr>
                    <tr id="trNutriSkrinning" runat="server" visible="false">
                        <td class="label">Nutrition Skrinning</td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="optNutritionSkrinning" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Good" Value="Good"></asp:ListItem>
                                <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
                                <asp:ListItem Text="Malnutrition" Value="Malnutrition"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
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
