# 👥 Client Management System — C# Windows Forms

![C#](https://img.shields.io/badge/Language-C%23-purple?style=flat-square&logo=csharp)
![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey?style=flat-square&logo=windows)
![Framework](https://img.shields.io/badge/Framework-.NET%204.7.2-blueviolet?style=flat-square)
![UI](https://img.shields.io/badge/UI-Windows%20Forms-blue?style=flat-square)
![Controls](https://img.shields.io/badge/Controls-ListView%20%2B%20ImageList%20%2B%20Timer-orange?style=flat-square)
![Status](https://img.shields.io/badge/Status-Complete-brightgreen?style=flat-square)
![License](https://img.shields.io/badge/License-MIT-yellow?style=flat-square)

A **desktop client management application** built with C# Windows Forms. Features a rich ListView with gender-based avatars, a live details panel that updates on selection, a real-time clock, 5 view modes, and a dedicated edit form — the most feature-rich WinForms project in the series.

---

## 📸 Preview

```
┌────────────────────────────────────────────────────────────────────┐
│  Client Management System          🕐 14:32:05   📅 05/12/2026    │
├──────────────────────┬─────────────────────────────────────────────┤
│  ┌─ Add Client ────┐ │  ID │ First Name │ Last Name │ Age │ Major  │
│  │ First Name [  ] │ │ 👨 1 │ Mohammed   │ Alami     │ 25  │ CS     │
│  │ Last Name  [  ] │ │ 👩 2 │ Sara       │ Benali    │ 22  │ Math   │
│  │ Age        [  ] │ │ 👨 3 │ Yassine    │ El Fassi  │ 28  │ Law    │
│  │ Major      [  ] │ │                                              │
│  │ Date    [picker]│ ├──────────────────────────────────────────────┤
│  │ ● Male  ○ Female│ │  📷 [Man.png]    ID        : 1              │
│  │ [Add] [Del][Edit│ │               First Name  : Mohammed        │
│  └─────────────────┘ │               Last Name   : Alami           │
│                      │               Age         : 25              │
│  View: ●Details      │               Major       : CS              │
│        ○List         │               Date        : 05/12/2026      │
│        ○SmallIcon    │                                              │
│        ○LargeIcon    │                                              │
│        ○Tile         │                                              │
└──────────────────────┴─────────────────────────────────────────────┘
```

---

## ✨ Features

- ➕ **Add clients** — First Name, Last Name, Age, Major, Date, Gender (with validation)
- 🗑️ **Delete client** — removes selected row from ListView
- ✏️ **Edit client** — opens Form2 pre-filled with selected client's data
- 👁️ **Live details panel** — updates instantly when a row is selected in ListView
- 🖼️ **Gender avatars** — Man.png / Women.png shown in ListView rows and detail panel
- ⏰ **Live clock + date** — real-time `HH:mm:ss` and `MM/dd/yyyy` via `Timer`
- 📋 **5 ListView view modes** — Details, List, SmallIcon, LargeIcon, Tile
- 🆔 **Auto-increment ID** — each new client gets a unique sequential ID
- 🗓️ **DateTimePicker** — date selection with calendar popup

---

## 🗂️ Project Structure

```
ClientManagement/
│
├── Program.cs              # Entry point
│
├── Form1.cs                # Main screen — add, delete, edit, list, details panel
├── Form1.Designer.cs       # Main UI layout
│
├── Form2.cs                # Edit client screen — pre-filled form + save/cancel
├── Form2.Designer.cs       # Edit form UI layout
│
├── Resources/
│   ├── Man.png             # Male avatar (used in ListView + details panel)
│   ├── Man1.png            # Alternative male avatar (ImageList LargeIcon)
│   └── Women.png           # Female avatar
│
└── README.md
```

---

## 🧱 Code Architecture

### Form1 — Main Screen

**Key controls:**

| Control | Type | Role |
|---|---|---|
| `listView1` | `ListView` | Main client list with columns and icons |
| `imageList1` | `ImageList` | Small icons (Man/Women) for Details/List/SmallIcon modes |
| `imageList2` | `ImageList` | Large icons for LargeIcon/Tile modes |
| `pictureBox1` | `PictureBox` | Shows gender avatar in the details panel |
| `dateTimePicker1` | `DateTimePicker` | Date input with calendar |
| `timer1` | `Timer` | 1-second tick → updates `lblTime` and `labelDate` |
| `rbDetails/List/SmallIcon/Large/Tile` | `RadioButton` | Switches `listView1.View` |
| `panel1` | `Panel` | Add form area (left side) |
| `panel2` | `Panel` | Details panel (bottom right) |

**Key methods:**

| Method | Description |
|---|---|
| `btnAdd_Click()` | Validates fields, auto-increments ID, creates `ListViewItem` with `ImageIndex` by gender, adds to `listView1`, clears inputs |
| `listView1_ItemSelectionChanged()` | Reads selected row's SubItems → updates all detail labels + `pictureBox1` |
| `timer1_Tick()` | Updates `lblTime` and `labelDate` every second with `DateTime.Now` |
| `btnDelete_Click()` | Calls `selectedItem.Remove()` on the selected `ListViewItem` |
| `btnEdit_Click()` | Passes selected `ListViewItem` to `Form2` via constructor → `ShowDialog()` |
| `rb*_CheckedChanged()` | Sets `listView1.View` to the corresponding `View` enum value |

### Form2 — Edit Client Screen

Receives the selected `ListViewItem` via constructor and writes back to it on Save:

```csharp
// Constructor — pre-fill fields from selected item
public Form2(ListViewItem Item) {
    InitializeComponent();
    selectedItem = Item;
    txtFirstName.Text        = selectedItem.SubItems[1].Text;
    txtLastName.Text         = selectedItem.SubItems[2].Text;
    txtAge.Text              = selectedItem.SubItems[3].Text;
    txtMajor.Text            = selectedItem.SubItems[4].Text;
    dateTimePicker1.Text     = selectedItem.SubItems[5].Text;
    rbMale.Checked           = selectedItem.ImageIndex == 0;
    rbFemale.Checked         = selectedItem.ImageIndex != 0;
}

// Save — write directly back to the ListViewItem (reference)
private void btnSave_Click(object sender, EventArgs e) {
    selectedItem.SubItems[1].Text = txtFirstName.Text;
    selectedItem.SubItems[2].Text = txtLastName.Text;
    // ... etc
    selectedItem.ImageIndex = rbMale.Checked ? 0 : 1;
    MessageBox.Show("Client Changed Info successfully.");
    Close();
}
```

> Since `ListViewItem` is passed **by reference**, changes in Form2 reflect instantly in Form1's ListView — no reload needed.

### ListView SubItems Index Map

| Index | Column | Example |
|---|---|---|
| 0 | ID | `"1"` |
| 1 | First Name | `"Mohammed"` |
| 2 | Last Name | `"Alami"` |
| 3 | Age | `"25"` |
| 4 | Major | `"CS"` |
| 5 | Date | `"05/12/2026"` |

---

## 🚀 Getting Started

### Prerequisites
- **Visual Studio 2019+**
- **.NET Framework 4.7.2**
- Windows OS

### Run
1. Open `MyFirstWindowsForm.sln`
2. Press `Ctrl + F5`

---

## 📊 Controls Used (Learning Reference)

| Control | Used For |
|---|---|
| `ListView` | Tabular data display with icons and multiple view modes |
| `ImageList` | Icon sets for ListView rows (small + large) |
| `PictureBox` | Avatar display in details panel |
| `DateTimePicker` | Date input with built-in calendar |
| `Timer` | Real-time clock update every 1 second |
| `RadioButton` | Gender selection + ListView view mode switching |
| `Panel` | Layout grouping (add form / details panel) |
| `GroupBox` | View mode selector grouping |

---

## 🔮 Possible Improvements

- [ ] Add **search/filter** bar to filter ListView by name
- [ ] **Persist data** to a file or database (currently in-memory only)
- [ ] Add **sort on column click** in ListView
- [ ] Add **input validation** for Age (numbers only) and required fields warning
- [ ] Add a **total clients count** label that updates on Add/Delete
- [ ] Add **export to CSV** button

---

## 👨‍💻 Author

> Built with ❤️ as part of a C# Windows Forms learning journey.

Feel free to fork, star ⭐, or contribute!

---

## 📄 License

This project is licensed under the **MIT License** — free to use and modify.
