import { Component } from '@angular/core';
import { StudentService } from '../../../services/student.service';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { studentMaster } from '../../../models/StudentMaster';

@Component({
  selector: 'app-student-form',
  templateUrl: './student-form.component.html',
  styleUrls: ['./student-form.component.css']
})
export class StudentFormComponent {
  studentList: studentMaster[]=[]
  studentForm: FormGroup | any;
  student: studentMaster = {} as studentMaster
  MxId: number = 0;
  mobilePattern = '/^\d+$/'
  saveBtn: boolean = false;
  updateBtn: boolean = false;
  deleteBtn: boolean = false;
  constructor(
    private studentService: StudentService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {

    this.getStudent();
    this.maxId();

    this.saveBtn = false;
    this.updateBtn = true;
    this.deleteBtn = true;
  }

  //get student all
  getStudent() {
    this.studentService.getStudentAll().subscribe((res) => {

      this.studentList = res;

    });

    this.maxId();
  }

  maxId() {
    this.studentService.maxId().subscribe((res) => {
      
      if (res === 0) {
        this.MxId = 1;
      } else {
        this.MxId = res;
        this.student.sId = res + 1;
      }
    })
  }

  //Save
  Save(studentForm: NgForm) {
    if (this.student.sName != '' && this.student.sEmail != '' && studentForm.valid) {
      this.student.sName = this.student.sName;
      this.student.sEmail = this.student.sEmail;
      this.student.age = this.student.age;
      this.student.contact = this.student.contact;

      //passing data to service
      this.studentService.addStudent(this.student).subscribe((res) => {

        this.clear();
        this.getStudent();
        this.maxId()

        alert("Saved !")

        this.saveBtn = false;
        this.updateBtn = true;
        this.deleteBtn = true;
      });
    } else {
      alert("Invalid Field");
    }
  }


  //update
  update(studentForm: NgForm) {
    if (this.student.sName != " " && this.student.sEmail != " ") {
      this.student.sId = this.student.sId;
      this.student.sName = this.student.sName;
      this.student.sEmail = this.student.sEmail;
      this.student.age = this.student.age;
      this.student.contact = this.student.contact;
      console.log(this.student);
      //passing data to service
      this.studentService.updateStudent(this.student).subscribe((res) => {
        this.clear();
        this.getStudent();
        this.maxId()

        alert("Updated !");

        this.saveBtn = false;
        this.updateBtn = true;
        this.deleteBtn = true;
      });
    } else {
      alert("Invalid Field");
    }
  }

  //get student by id
  getById(id: number) {
    this.studentService.getById(id).subscribe((res) => {
      this.student.sId = res.sId;
      this.student.sEmail = res.sEmail;
      this.student.sName = res.sName;
      this.student.age = res.age;
      this.student.contact = res.contact;

    });

    this.saveBtn = true
    this.updateBtn = false;
    this.deleteBtn=false
  }

  //Clear Input
  clear() {
    this.student.age = 0;
    this.student.sName = '';
    this.student.contact = '';
    this.student.sEmail = '';
   
    this.maxId();

    this.saveBtn = false;
    this.updateBtn = true;
    this.deleteBtn = true;
  }

  //delete 
  Delete() {
    if (this.student.sId != 0) {
      const Id = this.student.sId;
      this.studentService.deleteStudent(Id).subscribe((res) => {

        alert("Successfully Deleted");

        this.clear();
        this.getStudent();
        this.maxId();
      })
    }
  }
}
