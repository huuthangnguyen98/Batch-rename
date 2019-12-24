using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
//using DocumentFormat.OpenXml.Wordprocessing;
using ListViewItem = System.Windows.Controls.ListViewItem;

namespace Batch_Rename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// List tên files
        /// </summary>
        BindingList<FileInfo> fileNames = null;
        /// <summary>
        /// List tên folders
        /// </summary>
        BindingList<FolderInfo> folderNames = null;
        /// <summary>
        /// List các methods được dùng
        /// </summary>
        BindingList<Method> methods = null;
        /// <summary>
        /// 1 chuỗi để hash Settings vào preset
        /// </summary>
        const string seperator = "@1612618@Nguyen Huu Thang@1998@";
        /// <summary>
        /// Danh sách các Prest được load lên
        /// </summary>
        BindingList<string> presetsList = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        class FileInfo
        {
            public string Name { get; set; }
            public string Extension { get; set; }
            public string Path { get; set; }
            public string NewName { get; set; }
            public string Error { get; set; }
            public string NameNoExt { get; set; }
        }
        class FolderInfo
        {
            public string Name { get; set; }
            public string Path { get; set; }
            public string NewName { get; set; }
            public string Error { get; set; }
        }
        class Method
        {
           public string Name { get; set; }
           public string Setting { get; set; }
           public string HashSetting { get; set; }
        }
        class Replace : Method
        {
            public string WordFrom { get; set; }
            public string WordTo { get; set; }
            public int CaseId { get; set; }

            public Replace()
            {
                Name = "Replace";
            }
        }
        class ReplaceBus
        {
            
            public static string Replace (string preName, Replace rep)
            {
                var _newName = "";

                _newName = preName.Replace(rep.WordFrom, rep.WordTo);

                return _newName;
            }
        }
        class NewCase : Method
        {
            public int CaseId { get; set; }

            public NewCase()
            {
                Name = "NewCase";
            }
        }
        class NewCaseBus
        {
            public static string NewCase(string preName, NewCase nc)
            {
                var _newName = "";
                var _caseId = nc.CaseId;

                switch (_caseId)
                {
                    case 0:
                        {
                            _newName = preName.ToUpper();
                            break;
                        }
                    case 1:
                        {
                            _newName = preName.ToLower();
                            break;
                        }
                    case 2:
                        {
                            char[] a = preName.ToLower().ToCharArray();

                            for (int i = 0; i < a.Count(); i++)
                            {
                                a[i] = i == 0 || a[i - 1] == ' ' ? char.ToUpper(a[i]) : a[i];

                            }
                            _newName = new string(a);
                            break;
                        }

                }
                return _newName;
            }
        }
        class Move : Method
        {
            public int CaseId { get; set; }
            public int Start { get; set; }
            public int End { get; set; }

            public Move()
            {
                Name = "Move";
            }
        }
        class MoveBus
        {
            public static string Move(string preName, Move mov)
            {
                var _newName = "";
                var exName = preName;
                var _start = mov.Start -1;
                var _end = mov.End -1;
                var _caseId = mov.CaseId; //0 - begin, 1 - end
                var length = _end - _start + 1;

                var sub = preName.Substring(_start, length);
                _newName = preName.Remove(_start, length);
                if (_caseId == 0)
                {
                    _newName = sub + _newName;
                }
                else
                {
                    _newName = _newName + sub;
                }

                return _newName;
            }
        }
        class FullnameNormalize : Method
        {

            public FullnameNormalize()
            {
                Name = "FullnameNormalize";
            }
        }
        class FullnameNormalizeBus
        {
            public static string FullnameNormalize(string preName)
            {
                var _newName = "";

                _newName = preName.Trim();
                string twoSpace = "  ";
                while (_newName.IndexOf(twoSpace)>0)
                {
                    _newName = _newName.Replace(twoSpace, " ");
                }
                
                char[] a = _newName.ToLower().ToCharArray();

                for (int i = 0; i < a.Count(); i++)
                {
                    a[i] = i == 0 || a[i - 1] == ' ' ? char.ToUpper(a[i]) : a[i];

                }
                _newName = new string(a);
                return _newName;
            }
        }
        class UniqueName : Method
        {
            public UniqueName()
            {
                Name = "UniqueName";
            }
        }
        class UniqueNameBus
        {
            public static string UniqueName(string preName)
            {
                var _newName = "";

                var guid = Guid.NewGuid();
                _newName = guid.ToString();

                return _newName;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Binding data
            fileNames = new BindingList<FileInfo>();
            filesListview.ItemsSource = fileNames;

            folderNames = new BindingList<FolderInfo>();
            folderListview.ItemsSource = folderNames;

            methods = new BindingList<Method>();
            methodListview.ItemsSource = methods;

            // Nạp preset
            presetsList = new BindingList<string>();
            // Tạo file Preset.txt nếu chưa tồn tại
            if (!Directory.Exists("Preset"))
                Directory.CreateDirectory("Preset");
            string[] paths = Directory.GetFiles("Preset");

            foreach (string path in paths)
            {
                var length = (path as string).Length;
                // remove .txt và Preset\
                // Load name các Preste
                var name = (path as string).Remove(length - 4).Remove(0, 7);
                presetsList.Add(name);
            }

            presetCombobox.ItemsSource = presetsList;

        }

        // Lấy list tên file từ folder
        private void addFilesButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
  
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // lấy folder path
                    var folderPath = dialog.SelectedPath + @"\";
                    // Lấy ds files
                    string[] paths = Directory.GetFiles(folderPath);
                    // Covert to List<NameInfo>
                    foreach (string path in paths)
                    {
                        var nameInfo = new FileInfo()
                        {
                            Name = System.IO.Path.GetFileName(path),
                            Extension = System.IO.Path.GetExtension(path),
                            NameNoExt = System.IO.Path.GetFileNameWithoutExtension(path),
                            Path = folderPath
                        };
                        fileNames.Add(nameInfo);
                    }
                }
        }
        // Lấy danh sách thư mục con
        private void addFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // lấy folder path
                var folderPath = dialog.SelectedPath + @"\";
                // Lấy ds folders
                string[] paths = Directory.GetDirectories(folderPath);
                // Covert to List<NameInfo>
                foreach (string path in paths)
                {
                    var nameInfo = new FolderInfo()
                    {
                        Name = path.Remove(0, folderPath.Length),
                        Path = folderPath
                    };
                    folderNames.Add(nameInfo);
                }
            }
        }

        private void removeFile_Click(object sender, RoutedEventArgs e)
        {
            var name = filesListview.SelectedItem as FileInfo;
            fileNames.Remove(name);
        }

        private void removeAllFile_Click(object sender, RoutedEventArgs e)
        {
            fileNames.Clear();
        }

        private void removeFolder_Click(object sender, RoutedEventArgs e)
        {
            var name = folderListview.SelectedItem as FolderInfo;
            folderNames.Remove(name);
        }

        private void removeAllFolder_Click(object sender, RoutedEventArgs e)
        {
            folderNames.Clear();
        }
        
        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            fileNames.Clear();
            folderNames.Clear();
            methods.Clear();
        }
        /// <summary>
        /// //Kiểm tra xem có tên file nào trùng không
        /// </summary>
        public void checkDuplicationFileName()
        {
            var isDuplication = false;
            for (int i = 0; i < fileNames.Count; i++)
            {
                for (int j = i + 1; j < fileNames.Count; j++)
                    if (fileNames[i].NewName == fileNames[j].NewName)
                        isDuplication = true;
            }
            if (isDuplication)
            {
                var dup = new DuplicationDialog();
                dup.ShowDialog();
                var dupId = dup.caseId;
                if (dupId == 0)
                {
                    // Add number
                    for (int i = 0; i < fileNames.Count; i++)
                    {
                        var num = 1;
                        for (int j = i + 1; j < fileNames.Count; j++)
                            if (fileNames[i].NewName == fileNames[j].NewName)
                            {
                                var ext = fileNames[j].Extension;
                                var lenghtExt = ext.Length;
                                var leng = fileNames[j].NewName.Length;
                                var newNameWithoutExtension = fileNames[j].NewName.Remove(leng - lenghtExt, lenghtExt) + num.ToString();
                                num++;
                                fileNames[j].NewName = newNameWithoutExtension + ext;
                            }
                    }
                }
                else
                {
                    // Skip
                    foreach (var name in fileNames)
                    {
                        name.NewName = name.Name;
                    }
                }
            }
        }
        
        /// <summary>
        /// Hàm áp các method lên name => newName
        /// </summary>
        public void preBatchRename()
        {
            if (renameFilesTabItem.IsSelected)
            {
                //Rename file
                foreach (FileInfo name in fileNames)
                {
                    var item = name as FileInfo;
                    // Name without Extension
                    var result = name.NameNoExt;
                    var Ext = name.Extension;
                    foreach (var method in methods)
                    {
                        switch (method.Name)
                        {
                            case "Replace":
                                {
                                    var rep = method as Replace;
                                    try
                                    {
                                        // Kiểm tra apply trên name hay extension
                                        var caseId = rep.CaseId;
                                        if (caseId == 0)
                                        {
                                            //apply to name
                                            result = ReplaceBus.Replace(result, rep);
                                        }
                                        else
                                        {
                                            //apply to extension
                                            Ext = ReplaceBus.Replace(Ext, rep);
                                        }
                                    }
                                    catch
                                    {
                                        item.Error = "Error";
                                    }
                                    break;
                                }
                            case "NewCase":
                                {
                                    var nc = method as NewCase;
                                    try
                                    {
                                        result = NewCaseBus.NewCase(result, nc);
                                    }
                                    catch
                                    {
                                        item.Error = "Error";
                                    }
                                    break;
                                }
                            case "Move":
                                {
                                    var mov = method as Move;
                                    try
                                    {
                                        result = MoveBus.Move(result, mov);
                                    }
                                    catch
                                    {
                                        item.Error = "Error";
                                    }
                                    break;
                                }
                            case "FullnameNormalize":
                                {
                                    try
                                    {
                                        result = FullnameNormalizeBus.FullnameNormalize(result);
                                    }
                                    catch
                                    {
                                        item.Error = "Error";
                                    }
                                    break;
                                }
                            case "UniqueName":
                                {
                                    try
                                    {
                                        result = UniqueNameBus.UniqueName(result);
                                    }
                                    catch
                                    {
                                        item.Error = "Error";
                                    }
                                    break;
                                }
                        }
                    }
                    var _newName = result + Ext;
                    item.NewName = _newName;
                }

            }
            else
            {
                //Rename folder
                foreach (FolderInfo name in folderNames)
                {
                    var item = name as FolderInfo;
                    var result = name.Name;
                    foreach (var method in methods)
                    {
                        switch (method.Name)
                        {
                            case "Replace":
                                {
                                    var rep = method as Replace;
                                    try
                                    {
                                        result = ReplaceBus.Replace(result, rep);
                                    }
                                    catch
                                    {
                                        item.Error = "Error";
                                    }
                                    break;
                                }
                            case "NewCase":
                                {
                                    var nc = method as NewCase;
                                    try
                                    {
                                        result = NewCaseBus.NewCase(result, nc);
                                    }
                                    catch
                                    {
                                        item.Error = "Error";
                                    }
                                    break;
                                }
                            case "Move":
                                {
                                    var mov = method as Move;
                                    try
                                    {
                                        result = MoveBus.Move(result, mov);
                                    }
                                    catch
                                    {
                                        item.Error = "Error";
                                    }
                                    break;
                                }
                            case "FullnameNormalize":
                                {
                                    try
                                    {
                                        result = FullnameNormalizeBus.FullnameNormalize(result);
                                    }
                                    catch
                                    {
                                        item.Error = "Error";
                                    }
                                    break;
                                }
                            case "UniqueName":
                                {
                                    try
                                    {
                                        result = UniqueNameBus.UniqueName(result);
                                    }
                                    catch
                                    {
                                        item.Error = "Error";
                                    }
                                    break;
                                }
                        }
                    }
                    item.NewName = result;
                }
            }
        }
        private void previewButton_Click(object sender, RoutedEventArgs e)
        {
            preBatchRename();

            fileNames.ResetBindings();
            folderNames.ResetBindings();

        }
        private void startBatchButton_Click(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("Do you want to start batch rename ?", "Start Batch", MessageBoxButton.YesNo);
            
            if (result == MessageBoxResult.Yes)
            {
                preBatchRename();

                if (renameFilesTabItem.IsSelected)
                {
                    // Rename Files
                    checkDuplicationFileName();

                    foreach (var name in fileNames)
                    {
                        var path = name.Path;
                        try
                        {
                            System.IO.File.Move(path + name.Name, path + name.NewName);
                            name.Error = "Success";
                        }
                        catch (IOException)
                        {
                            name.Error = "Error";
                        }
                    }
                    fileNames.ResetBindings();
                }
                else
                {
                    // Rename Folders -- Chưa cài đặt
                    checkDuplicationFolderName();
                    foreach (var name in folderNames)
                    {
                        var path = name.Path;
                        try
                        {
                            // Kiểm tra xem  newNamefolder có bị trùng với nameFolder cũ hay không (KHÔNG KỂ HOA THƯỜNG)
                            if (name.Name.ToUpper() != name.NewName.ToUpper())
                            {
                                System.IO.Directory.Move(path + name.Name, path + name.NewName);
                            }
                            else
                            {
                                // Tạm thời đổi tên thành chuỗi seperator
                                System.IO.Directory.Move(path + name.Name, path + seperator);
                                // Đổi tiếp thành newName
                                System.IO.Directory.Move(path + seperator, path + name.NewName);
                            }
                            name.Error = "Success";
                        }
                        catch (IOException ex)
                        {
                            name.Error = "Error";
                            System.Windows.MessageBox.Show(ex.ToString());
                        }
                    }
                    folderNames.ResetBindings();
                }
            }
        }

        public void checkDuplicationFolderName()
        {
            // Chưa cài đặt
        }

        private void removeMethod_Click(object sender, RoutedEventArgs e)
        {
            var method = methodListview.SelectedItem as Method;
            methods.Remove(method);
        }

        private void removeAllMethod_Click(object sender, RoutedEventArgs e)
        {
            methods.Clear();
        }
        /// <summary>
        /// Hàm thêm method Replace vào methods
        /// </summary>
        /// <param name="_wordFrom"></param>
        /// <param name="_wordTo"></param>
        /// <param name="_caseId">0 - Apply to Name, 1 - Apply to Extension</param>
        public void addReplace(string _wordFrom, string _wordTo, int _caseId)
        {
            var _s = "";
            if (_caseId == 0)
            {
                _s = " (Name)";
            }
            else
            {
                _s = " (Extension)";
            }
            var newMethod = new Replace()
            {
                WordFrom = _wordFrom,
                WordTo = _wordTo,
                CaseId = _caseId,
                Setting = $"Replace - From \"{_wordFrom}\" to \"{_wordTo}\"{_s}",
                HashSetting = $"Replace{seperator}{_wordFrom}{seperator}{_wordTo}{seperator}{_caseId}"
            };
            methods.Add(newMethod);
        }
        /// <summary>
        /// Hàm thêm method NewCase vào methods
        /// </summary>
        /// <param name="_caseId">0 - Up all, 1 - Low all, 2 - Up first</param>
        public void addNewCase(int _caseId)
        {
            string _setting = "New Case - ";
            switch (_caseId)
            {
                case 0:
                    {
                        _setting += "Upper Case all characters";
                        break;
                    }
                case 1:
                    {
                        _setting += "Lower Case all characters";
                        break;
                    }
                case 2:
                    {
                        _setting += "Upper Case the first characters of the words";
                        break;
                    }
            }
            var newMethod = new NewCase()
            {
                CaseId = _caseId,
                Setting = _setting,
                HashSetting = $"NewCase{seperator}{_caseId}"
            };
            methods.Add(newMethod);
        }
        /// <summary>
        /// Hàm thêm method FullnameNornmalize vào methods
        /// </summary>
        public void addFullnameNornmalize()
        {
            var newMethod = new FullnameNormalize()
            {
                Setting = "Fullname Normalize",
                HashSetting = "FullnameNormalize"
            };
            methods.Add(newMethod);
        }
        /// <summary>
        /// Hàm thêm methods Move vào methods
        /// </summary>
        /// <param name="_caseId">0 - to begin, 1 - to end</param>
        /// <param name="_start"></param>
        /// <param name="_end"></param>
        public void addMove(int _caseId, int _start, int _end)
        {
            string _setting = "Move - ";
            switch (_caseId)
            {
                case 0:
                    {
                        _setting += $"Move {_start} - {_end} To begin";
                        break;
                    }
                case 1:
                    {
                        _setting += $"Move {_start} - {_end} To end";
                        break;
                    }
            }

            var newMethod = new Move()
            {
                CaseId = _caseId,
                Setting = _setting,
                Start = _start,
                End = _end,
                HashSetting = $"Move{seperator}{_start}{seperator}{_end}{seperator}{_caseId}"
            };
            methods.Add(newMethod);
        }
        /// <summary>
        /// Hàm thêm method Unique
        /// </summary>
        public void addUniqueName()
        {
            var newMethod = new UniqueName()
            {
                Setting = "Unique Name - Change to GUID",
                HashSetting = "UniqueName"
            };
            methods.Add(newMethod);
        }
        private void AddMethod_Click(object sender, RoutedEventArgs e)
        {
            int _methodId = methodCombobox.SelectedIndex;
            switch (_methodId)
            {
                case 1:
                    {
                        //add replace
                        var replaceDialog = new ReplaceDialog();
                        replaceDialog.ShowDialog();
                        if (replaceDialog.DialogResult == true)
                        {
                            string _wordFrom = replaceDialog.wordFrom;
                            string _wordTo = replaceDialog.wordTo;
                            int _caseId = replaceDialog.caseId;

                            addReplace(_wordFrom, _wordTo, _caseId);
                        }
                        break;
                    }
                case 2:
                    {
                        //newcase
                        var newCaseDialog = new NewCaseDialog();
                        newCaseDialog.ShowDialog();
                        if (newCaseDialog.DialogResult == true)
                        {
                            int _caseId = newCaseDialog.caseId;
                            addNewCase(_caseId);
                        }
                        break;
                    }
                case 3:
                    {
                        //fullname normalize
                        addFullnameNornmalize();
                        break;
                    }
                case 4: 
                    {
                        //move
                        var moveDialog = new MoveDialog();
                        moveDialog.ShowDialog();
                        if (moveDialog.DialogResult == true)
                        {
                            int _caseId = moveDialog.caseId;
                            int _start = moveDialog.start;
                            int _end = moveDialog.end;
                            addMove(_caseId, _start, _end);
                        }
                        break;
                    }
                case 5:
                    {
                        //unique name
                        addUniqueName();
                        break;
                    }
            }
        }
       
        private void AddPreset_Click(object sender, RoutedEventArgs e)
        {
            
            if (presetCombobox.SelectedItem != null)
            {
                var namePreset = presetCombobox.SelectedItem.ToString();
                var presetFile = "Preset\\" + namePreset +".txt";
                var lines = File.ReadAllLines(presetFile);

                foreach (var line in lines)
                {
                    string[] tokens = line.Split(new string[] { seperator }, StringSplitOptions.None);
                    var name = tokens[0];
                    switch (name)
                    {
                        case "Replace":
                            {
                                var _wordFrom = tokens[1];
                                var _wordTo = tokens[2];
                                var _caseId = int.Parse(tokens[3]);
                                addReplace(_wordFrom, _wordTo, _caseId);
                                break;
                            }
                        case "NewCase":
                            {
                                var _caseId = int.Parse(tokens[1]);
                                addNewCase(_caseId);
                                break;
                            }
                        case "FullnameNormalize":
                            {
                                addFullnameNornmalize();
                                break;
                            }
                        case "Move":
                            {
                                var _start = int.Parse(tokens[1]);
                                var _end = int.Parse(tokens[2]);
                                var _caseId = int.Parse(tokens[3]);

                                addMove(_caseId, _start, _end);
                                break;
                            }
                        case "UniqueName":
                            {
                                addUniqueName();
                                break;
                            }
                    }
                }
            }
        }

        private void savePresetButton_Click(object sender, RoutedEventArgs e)
        {
            if (methods.Count > 0)
            {
                var presetDialog = new PresetDialog();
                presetDialog.ShowDialog();
                if (presetDialog.DialogResult == true)
                {
                    var name = presetDialog.namePreset;
                    
                        using (var file = File.CreateText($"Preset\\{name}.txt"))
                        {
                            foreach (var me in methods)
                            {
                                file.WriteLine(me.HashSetting.ToString());
                            }
                            presetsList.Add(name);
                        }
                    
                }
            }
        }
        /// <summary>
        /// Hàm edit các method trên list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editMethod_Click(object sender, RoutedEventArgs e)
        {
            //int _methodId = methodCombobox.SelectedIndex;
            var method = methodListview.SelectedItem as Method;
            if (method != null)
            {
                var _methodName = method.Name;
                switch (_methodName)
                {
                    case "Replace":
                        {
                            var rep = method as Replace;
                            var _wordFrom = rep.WordFrom;
                            var _wordTo = rep.WordTo;
                            var _caseId = rep.CaseId;

                            var replaceDialog = new ReplaceDialog(_wordFrom, _wordTo, _caseId);
                            replaceDialog.ShowDialog();
                            if (replaceDialog.DialogResult == true)
                            {
                                //changed data
                                _wordFrom = replaceDialog.wordFrom;
                                _wordTo = replaceDialog.wordTo;
                                _caseId = replaceDialog.caseId;

                                var _s = "";
                                if (_caseId == 0)
                                {
                                    _s = " (Name)";
                                }
                                else
                                {
                                    _s = " (Extension)";
                                }

                                rep.WordFrom = _wordFrom;
                                rep.WordTo = _wordTo;
                                rep.CaseId = _caseId;
                                rep.Setting = $"Replace - From \"{_wordFrom}\" to \"{_wordTo}\"{_s}";
                                rep.HashSetting = $"Replace{seperator}{_wordFrom}{seperator}{_wordTo}{seperator}{_caseId}";
                            }
                            break;
                        }
                    case "NewCase":
                        {
                            var nc = method as NewCase;
                            var _caseId = nc.CaseId;
                            var newCaseDialog = new NewCaseDialog(_caseId);
                            newCaseDialog.ShowDialog();
                            if (newCaseDialog.DialogResult == true)
                            {
                                _caseId = newCaseDialog.caseId;

                                string _setting = "New Case - ";
                                switch (_caseId)
                                {
                                    case 0:
                                        {
                                            _setting += "Upper Case all characters";
                                            break;
                                        }
                                    case 1:
                                        {
                                            _setting += "Lower Case all characters";
                                            break;
                                        }
                                    case 2:
                                        {
                                            _setting += "Upper Case the first characters of the words";
                                            break;
                                        }
                                }

                                nc.CaseId = _caseId;
                                nc.Setting = _setting;
                                nc.HashSetting = $"NewCase{seperator}{_caseId}";
                            }
                            break;
                        }
                    case "FullnameNormalize":
                        {
                            //Show nothing
                            break;
                        }
                    case "Move":
                        {
                            var mov = method as Move;
                            var _start = mov.Start;
                            var _end = mov.End;
                            var _caseId = mov.CaseId;

                            var moveDialog = new MoveDialog(_caseId, _start, _end);
                            moveDialog.ShowDialog();
                            if (moveDialog.DialogResult == true)
                            {
                                //changed data
                                _start = moveDialog.start;
                                _end = moveDialog.end;
                                _caseId = moveDialog.caseId;

                                string _setting = "Move - ";
                                switch (_caseId)
                                {
                                    case 0:
                                        {
                                            _setting += $"Move {_start} - {_end} To begin";
                                            break;
                                        }
                                    case 1:
                                        {
                                            _setting += $"Move {_start} - {_end} To end";
                                            break;
                                        }
                                }

                                mov.CaseId = _caseId;
                                mov.Setting = _setting;
                                mov.Start = _start;
                                mov.End = _end;
                                mov.HashSetting = $"Move{seperator}{_start}{seperator}{_end}{seperator}{_caseId}";
                            }
                            break;
                        }
                    case "UniqueName":
                        {
                            //Show nothing
                            break;
                        }
                }
                methods.ResetBindings();
            } 
            
        }

        private void editPresetListButton_Click(object sender, RoutedEventArgs e)
        {
            var editPreset = new editPresetList(presetsList);
            editPreset.ShowDialog();
        }

        private void Top_Click(object sender, RoutedEventArgs e)
        {
            if (methodListview.SelectedItem != null)
            {
                var item = methodListview.SelectedItem as Method;
                var index = methodListview.SelectedIndex;
                var newIndex = 0;
                methods.RemoveAt(index);
                methods.Insert(newIndex, item);

                methodListview.SelectedIndex = newIndex;
            }
           
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            if (methodListview.SelectedItem != null)
            {
                var item = methodListview.SelectedItem as Method;
                var index = methodListview.SelectedIndex;
                var newIndex = index - 1;
                if (newIndex < 0)
                    newIndex = 0;
                methods.RemoveAt(index);
                methods.Insert(newIndex, item);

                methodListview.SelectedIndex = newIndex;
            }
                
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            if (methodListview.SelectedItem != null)
            {
                var item = methodListview.SelectedItem as Method;
                var index = methodListview.SelectedIndex;
                var newIndex = index + 1;
                if (newIndex > methods.Count - 1)
                    newIndex = methods.Count - 1;
                methods.RemoveAt(index);
                methods.Insert(newIndex, item);

                methodListview.SelectedIndex = newIndex;
            }
                
        }

        private void Bottom_Click(object sender, RoutedEventArgs e)
        {
            if (methodListview.SelectedItem != null)
            {
                var item = methodListview.SelectedItem as Method;
                var index = methodListview.SelectedIndex;
                var newIndex = methods.Count - 1;
                methods.RemoveAt(index);
                methods.Insert(newIndex, item);

                methodListview.SelectedIndex = newIndex;
            }
                
        }
    }
}
