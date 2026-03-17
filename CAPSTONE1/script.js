let currentRole = "teacher";

const students = [
  { id: "STUD001", first: "Juan", middle: "P.", last: "Dela Cruz" },
  { id: "STUD002", first: "Maria", middle: "L.", last: "Santos" }
];

const teachers = [
  { id: "TCH-001", first: "Reymark", middle: "C.", last: "Langomez" }
];

const strands = ["ICT", "STEM"];
const sections = [
  { grade: "11", section: "Emerald", strand: "ICT" }
];

const attendanceRecords = [];

function setRole(role, button) {
  currentRole = role;
  document.getElementById("loginTitle").textContent =
    role === "teacher" ? "Teacher Login" : "ATTENDANCE";

  document.querySelectorAll(".role-btn").forEach(btn => btn.classList.remove("active"));
  button.classList.add("active");
}

function login() {
  const idInput = document.getElementById("loginId");
  const idError = document.getElementById("idError");

  // Check if empty
  if (idInput.value.trim() === "") {
    // Show the text
    idError.style.display = "block";
    idInput.style.border = "1px solid red"; // Optional: turn the box red
    idInput.focus();
    return; 
  } else {
    // Hide it if they finally typed something
    idError.style.display = "none";
    idInput.style.border = ""; 
  }

  // ... rest of your login logic
  document.getElementById("loginPage").classList.remove("active");
  document.getElementById("appPage").classList.add("active");
  
  if (currentRole === "teacher") {
    loadTeacherMenu();
    showSection("teacher-dashboard");
  } else {
    loadAdminMenu();
    showSection("admin-dashboard");
  }
  renderAll();
}

function logout() {
  document.getElementById("appPage").classList.remove("active");
  document.getElementById("loginPage").classList.add("active");

  // Reset inputs and hide error message
  document.getElementById("loginId").value = "";
  document.getElementById("loginPassword").value = "";
  document.getElementById("idError").style.display = "none";
  document.getElementById("loginId").style.border = "";
}

function loadTeacherMenu() {
  document.getElementById("sidebarTitle").textContent = "Teacher";
  const menu = document.getElementById("menuList");
  menu.innerHTML = `
    <li class="active" onclick="menuClick(this, 'teacher-dashboard')">Dashboard</li>
    <li onclick="menuClick(this, 'teacher-students')">Students</li>
    <li onclick="menuClick(this, 'teacher-attendance')">Attendance</li>
  `;
}

function loadAdminMenu() {
  document.getElementById("sidebarTitle").textContent = "Admin Panel";
  const menu = document.getElementById("menuList");
  menu.innerHTML = `
    <li class="active" onclick="menuClick(this, 'admin-dashboard')">Dashboard</li>
    <li onclick="menuClick(this, 'admin-teachers')">Teachers</li>
    <li onclick="menuClick(this, 'admin-strands')">Strands & Sections</li>
  `;
}

function menuClick(element, sectionId) {
  document.querySelectorAll("#menuList li").forEach(li => li.classList.remove("active"));
  element.classList.add("active");
  showSection(sectionId);
}

function showSection(sectionId) {
  document.querySelectorAll(".content-section").forEach(section => {
    section.classList.remove("active");
  });
  document.getElementById(sectionId).classList.add("active");
}

function addStudent() {
  const id = document.getElementById("studentId").value.trim();
  const first = document.getElementById("studentFirst").value.trim();
  const middle = document.getElementById("studentMiddle").value.trim();
  const last = document.getElementById("studentLast").value.trim();

  if (!id || !first || !last) {
    alert("Please fill in Student ID, First Name, and Last Name.");
    return;
  }

  students.push({ id, first, middle, last });

  document.getElementById("studentId").value = "";
  document.getElementById("studentFirst").value = "";
  document.getElementById("studentMiddle").value = "";
  document.getElementById("studentLast").value = "";

  renderStudents();
  renderDashboardCounts();
}

function deleteStudent(index) {
  students.splice(index, 1);
  renderStudents();
  renderDashboardCounts();
}

function addTeacher() {
  const id = document.getElementById("teacherId").value.trim();
  const first = document.getElementById("teacherFirst").value.trim();
  const middle = document.getElementById("teacherMiddle").value.trim();
  const last = document.getElementById("teacherLast").value.trim();

  if (!id || !first || !last) {
    alert("Please fill in ID, First Name, and Last Name.");
    return;
  }

  teachers.push({ id, first, middle, last });

  document.getElementById("teacherId").value = "";
  document.getElementById("teacherFirst").value = "";
  document.getElementById("teacherMiddle").value = "";
  document.getElementById("teacherLast").value = "";

  renderTeachers();
  renderDashboardCounts();
}

function addStrand() {
  const strand = document.getElementById("strandInput").value.trim();
  if (!strand) {
    alert("Enter a strand.");
    return;
  }

  strands.push(strand);
  document.getElementById("strandInput").value = "";
  renderStrands();
  renderDashboardCounts();
}

function addSection() {
  const grade = document.getElementById("gradeInput").value.trim();
  const section = document.getElementById("sectionInput").value.trim();
  const strand = document.getElementById("sectionStrandInput").value.trim();

  if (!grade || !section || !strand) {
    alert("Complete all section fields.");
    return;
  }

  sections.push({ grade, section, strand });

  document.getElementById("gradeInput").value = "";
  document.getElementById("sectionInput").value = "";
  document.getElementById("sectionStrandInput").value = "";

  renderSections();
}

function markAttendance() {
  const enteredId = document.getElementById("attendanceInput").value.trim();
  const student = students.find(s => s.id.toLowerCase() === enteredId.toLowerCase());

  if (!student) {
    alert("Student not found.");
    return;
  }

  const now = new Date();
  const status = now.getHours() < 8 ? "Present" : "Late";

  attendanceRecords.unshift({
    id: student.id,
    name: `${student.first} ${student.last}`,
    time: now.toLocaleTimeString(),
    status
  });

  document.getElementById("attendanceInput").value = "";
  renderAttendance();
  renderDashboardCounts();
}

function renderStudents() {
  const tbody = document.getElementById("studentTableBody");
  tbody.innerHTML = "";

  students.forEach((student, index) => {
    tbody.innerHTML += `
      <tr>
        <td>${student.id}</td>
        <td>${student.first} ${student.middle} ${student.last}</td>
        <td>${student.id}</td>
        <td><button class="delete-btn" onclick="deleteStudent(${index})">Delete</button></td>
      </tr>
    `;
  });
}

function renderTeachers() {
  const tbody = document.getElementById("teacherTableBody");
  tbody.innerHTML = "";

  teachers.forEach(teacher => {
    tbody.innerHTML += `
      <tr>
        <td>${teacher.id}</td>
        <td>${teacher.first} ${teacher.middle} ${teacher.last}</td>
      </tr>
    `;
  });
}

function renderStrands() {
  const tbody = document.getElementById("strandTableBody");
  tbody.innerHTML = "";

  strands.forEach(strand => {
    tbody.innerHTML += `
      <tr>
        <td>${strand}</td>
      </tr>
    `;
  });
}

function renderSections() {
  const tbody = document.getElementById("sectionTableBody");
  tbody.innerHTML = "";

  sections.forEach(item => {
    tbody.innerHTML += `
      <tr>
        <td>${item.grade}</td>
        <td>${item.section}</td>
        <td>${item.strand}</td>
      </tr>
    `;
  });
}

function renderAttendance() {
  const tbody = document.getElementById("attendanceTableBody");
  tbody.innerHTML = "";

  attendanceRecords.forEach(record => {
    tbody.innerHTML += `
      <tr>
        <td>${record.id}</td>
        <td>${record.name}</td>
        <td>${record.time}</td>
        <td class="${record.status === "Present" ? "status-present" : "status-late"}">${record.status}</td>
      </tr>
    `;
  });
}

function renderDashboardCounts() {
  document.getElementById("teacherTotalStudents").textContent = students.length;
  document.getElementById("teacherPresentToday").textContent =
    attendanceRecords.filter(r => r.status === "Present").length;
  document.getElementById("teacherLateToday").textContent =
    attendanceRecords.filter(r => r.status === "Late").length;

  document.getElementById("adminTeacherCount").textContent = teachers.length;
  document.getElementById("adminStudentCount").textContent = students.length;
  document.getElementById("adminStrandCount").textContent = strands.length;
}

function renderAll() {
  renderStudents();
  renderTeachers();
  renderStrands();
  renderSections();
  renderAttendance();
  renderDashboardCounts();
}

renderAll();