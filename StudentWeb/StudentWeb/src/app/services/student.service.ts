import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environment/environment'
import { studentMaster } from '../models/StudentMaster';
@Injectable({
  providedIn: 'root'
})
export class StudentService {

  studentUrl = environment.apiUrl +'Student/Student';
  constructor(private http: HttpClient) { }
  getStudentAll(): Observable<any> {
    return this.http.get(this.studentUrl);
  }
  getById(id: number): Observable<studentMaster> {
    return this.http.get<studentMaster>(this.studentUrl + '/' + id);
  }
  addStudent(student: studentMaster): Observable<studentMaster> {
    return this.http.post<studentMaster>(this.studentUrl + '/Add', student);
  }
  updateStudent(student: studentMaster): Observable<studentMaster> {
    
    return this.http.put<studentMaster>(this.studentUrl + '/Update/' + student.sId, student)
  }
  deleteStudent(id: number): Observable<any> {
    return this.http.delete(this.studentUrl + '/Delete/' + id);
  }
  maxId(): Observable<any> {
    return this.http.get(this.studentUrl + '/MaxId');
  }
}
