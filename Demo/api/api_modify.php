<?php
if (isset($_GET['what'])) {
    $con = new mysqli("localhost","root","","train_placement_scet") or die();

    $con->set_charset("utf8");

    if (isset($_REQUEST['session']) && $_REQUEST['session'] != '') {
        $session = $_REQUEST['session'];
    } else {
        $session = $con->query("SELECT * FROM `sessions` WHERE `default_year`=1")->fetch_array();
        $session = $session['id'];
    }
    // $resp['success']=false;
    // $resp['message']="No api found";
    //Get app verion
    if ($_REQUEST['what']=="getversion") {
        $qry = "SELECT `version` FROM `version`";
        $version = $con->query($qry)->fetch_assoc();
        $resp['success']=true;
        $resp['version']=$version['version'];
    }
    //Sessions
    //addsession
    elseif ($_REQUEST['what'] == "addsession") {
        $label = $_POST['label'];
        $start_date = $_POST['start_date'];
        $end_date = $_POST['end_date'];
        $check = $con->query("SELECT * FROM `sessions` WHERE `label`='$label'");
        if ($check->num_rows > 0) {
            $resp['success'] = true;
            $resp['message'] = "Session already exists";
        } else {
            $query = "INSERT INTO `sessions`(`start_date`,`end_date`,`label`)
                                VALUE('$start_date','$end_date','$label')";
            $result = $con->query($query);
            if ($result > 0) {
                $resp['success'] = true;
                $resp['message'] = "Session added successfully";
            } else {
                $resp['success'] = true;
                $resp['message'] = "Could not insert session";
            }
        }
    }
    //Get session
    elseif ($_REQUEST['what'] == "getsession") {
        $qry = "SELECT * FROM `sessions` WHERE `status`!=3";
        if (isset($_REQUEST['id'])) {
            $qry .= " AND `id`=$_REQUEST[id]";
        }
        $result=$con->query($qry);
        $sessions=array();
        while ($row=$result->fetch_assoc()) {
            $sessions[]=$row;
        }
        $resp['success']=true;
        $resp['session'] = json_encode($session);
    }
    //Update session
    elseif ($_REQUEST['what'] == "updatesession") {
        $id = $_REQUEST['id'];
        $label = $_POST['label'];
        $start_date = $_POST['start_date'];
        $end_date = $_POST['end_date'];
        $check = $con->query("SELECT * FROM `sessions` WHERE `id`!=$id AND`label`='$label'  AND `status`!=3");
        if ($check->num_rows > 0) {
            $resp['success'] = true;
            $resp['message'] = "Another session already exists with same label";
        } else {
            $qry = "UPDATE `sessions`
                SET `start_date`='$start_date',`end_date`='$end_date',`label`='$label' WHERE `id`=$id";
            $result = $con->query($qry);
            if ($result) {
                $resp['success'] = true;
                $resp['message'] = "Session updated successfully";
            } else {
                $resp['success'] = false;
                $resp['message'] = "Could not update session";
            }
        }
    }
    //set default session
    elseif ($_REQUEST['what'] == "defaultsession") {
        $id = $_REQUEST['id'];
        $check = $con->query("SELECT * FROM `sessions` WHERE `id`=$id");
        $con->autocommit(false);
        if ($check->num_rows > 0) {
            $result = $con->query("UPDATE `sessions` SET `default_year`=1 WHERE `id`=$id");
            $result2 = $con->query("UPDATE `sessions` SET `default_year`=0 WHERE `id`!=$id");
            if ($result && $result2) {
                $con->commit();
                $resp['success'] = true;
                $resp['message'] = "Default session set successfully";
            } else {
                $con->rollback();
                $resp['success'] = false;
                $resp['message'] = "Failed to set default session";
            }
        } else {
            $resp['success'] = false;
            $resp['message'] = "Selected session does not exists";
        }
    }

    //user
    //insert user as a student
    elseif ($_REQUEST['what'] == "set_user") {
        //Insert User Code
        $data = json_decode(file_get_contents('php://input'), true);

        $column = "";
        $columnfirst = true;
        $values = "";
        for($i=0; $i<count($data); $i++){
            foreach($data[$i] as $key=>$value){
                if($key == "id" || $value == $data[$i]['id']){
                    continue;
                }else{
                    if($columnfirst){
                        $column.=" `$key` ";
                        $values.=" '$value' ";
                        $columnfirst = false;
                    }else{
                        $column.=" ,`$key` ";
                        $values.=" ,'$value' ";
                        $columnfirst = false;
                    }
                }
            }
            $q[$i] = "insert into users ($column) VALUES ($values)";
        }
        $resp = $column;
        // for ($i=0; $i<count($q); $i++){
        //     $query = $q[$i];
        //     $res = mysqli_query($con,$query);
        //     if($res){
        //         $resp['status']='Inserted';
        //     }else{
        //         $resp['status']='Error';
        //         break;
        //     }
        // }
    }
    //Student
    //Insert Student
    elseif ($_REQUEST['what'] == "set_student") {
        //Insert Student Code
        $data = json_decode(file_get_contents('php://input'), true);

        for($i=0; $i<count($data); $i++){
            $column = "";
            $columnfirst = true;
            $values = "";
            foreach($data[$i] as $key=>$value){
                if($key == "id" || $value == $data[$i]['id']){
                    continue;
                }else{
                    if($columnfirst){
                        $column.=" `$key` ";
                        $values.=" '$value' ";
                        $columnfirst = false;
                    }else{
                        $column.=" ,`$key` ";
                        $values.=" ,'$value' ";
                        $columnfirst = false;
                    }
                }
            }
            $q[$i] = "insert into student ($column) VALUES ($values)";
        }
        for ($i=0; $i<count($q); $i++){
            $query = $q[$i];
            $res = mysqli_query($con,$query);
            if($res){
                $resp['status']='Inserted';
            }else{
                $resp['status']='Error';
                break;
            }
        }
    }

    elseif ($_REQUEST['what'] == "getstudentdisplay") {
        $query = "SELECT
        `u`.`id` AS `id`,
        `u`.`name` AS `name`,
        `u`.`email` AS `email`,
        `s`.`surname` AS `surname`,
        `s`.`first_name` AS `first_name`,
        `s`.`last_name` AS `last_name`,
        `s`.`enrollment` AS `enrollment`
        FROM   `users` `u` ,`student` `s`
        WHERE `s`.`user_id`=`u`.`id`";
        if (isset($_REQUEST['pg_departmentid'])) {
            $query .= " AND `s`.`pg_departmentid`='$_REQUEST[pg_departmentid]'";
        }
        if (isset($_REQUEST['pg_sectorid'])) {
            $query .= " AND `s`.`pg_sectorid`='$_REQUEST[pg_sectorid]'";
        }
        if (isset($_REQUEST['id'])) {
            $query .= " AND `u`.`id`='$_REQUEST[id]'";
        }
        $query .= " AND `s`.`session_id`='$session'";
        $data = $con->query($query);
        $students = array();
        while ($row = $data->fetch_assoc()) {
            $students[] = $row;
        }
        $resp['success']=true;
        $resp['student']=json_encode($students);
    }

    elseif ($_REQUEST['what'] == "getstudentalldetails") {
        $query = "SELECT * FROM `users` `u`, `student` `s` WHERE `s`.`user_id`=`u`.`id`";
        if (isset($_REQUEST['pg_departmentid'])) {
            $query .= " AND `s`.`pg_departmentid`='$_REQUEST[pg_departmentid]'";
        }
        if (isset($_REQUEST['pg_sectorid'])) {
            $query .= " AND `s`.`pg_sectorid`='$_REQUEST[pg_sectorid]'";
        }
        $query .= " AND `s`.`session_id`='$session'";
        if (isset($_REQUEST['id'])) {
            $query.= " AND `u`.`id`='$_REQUEST[id]'";
        }
        $data = $con->query($query);
        $students = array();
        $i = 0;
        while ($student = $data->fetch_assoc()) {
            $students[$i] = $student;
            $work_experiances = $con->query("SELECT * FROM `work_experiances` WHERE `student_id`='$student[user_id]'");
            while ($work_experiance = $work_experiances->fetch_assoc()) {
                $students[$i]['work_experiances'][] = $work_experiance;
            }
            $internships = $con->query("SELECT * FROM `internships` WHERE `student_id`='$student[user_id]'");
            while ($internship = $internships->fetch_assoc()) {
                $students[$i]['internships'][] = $internship;
            }
            $additional_qualifications = $con->query("SELECT * FROM `additional_qualifications`
                                                        WHERE `student_id`='$student[user_id]'");
            while ($additional_qualification = $additional_qualifications->fetch_assoc()) {
                $students[$i]['additional_qualifications'][] = $internship;
            }
            $i++;
        }
        $resp['success']=true;
        $resp['student']=json_encode($students);
    }
    //Update student details
    elseif ($_REQUEST['what'] == "updatestudentdetails") {
        $set="";
        $setfirst=false;
        $where="";
        $wherefirst=false;
        foreach ($_POST as $key => $value) {
            if ($key =="id") {
                if ($wherefirst) {
                    $where.=" AND `$key`='$value'";
                } else {
                    $where.=" WHERE `$key`='$value'";
                }
            } else {
                if ($setfirst) {
                    $set.=", `$key`='$value'";
                    $setfirst=true;
                } else {
                    $set.=" `$key`='$value'";
                    $setfirst=true;
                }
            }
        }
        if ($setfirst && $wherefirst) {
            $query="UPDATE `student` SET $set $where";
            $result=$con->query($query);
            if ($result>0) {
                $resp['success']=true;
                $resp['message']="Student successfully updated";
            }else {
                $resp['success']=false;
                $resp['message']="There was a problem updating student please try again";
            }
        }else {
            $resp['success']=false;
            if (!$setfirst) {
                $resp['message']="Please enter atleast one field to update.";
            }else {
                $resp['message']="Please send id of student.";
            }
        }
    }
    //get Tenth details of students
    if ($_REQUEST['what'] == "gettendetails") {
        $query = "SELECT
        `id`,
        `ten_school`,
        `ten_passyear`,
        `ten_schooladdress`,
        `ten_schoolcity`,
        `ten_schoolpincode`,
        `ten_board`,
        `ten_score`,
        `ten_scoreoutof`,
        `ten_gapno`,
        `ten_gapyears`,
        `ten_admissionquota`
        FROM   `student`
        WHERE `session_id`='$session'";
        if (isset($_REQUEST['id'])) {
            $query .= " AND `user_id`=$_REQUEST[id]";
        }
        $data = $con->query($query);
        $students = array();
        while ($row = $data->fetch_assoc()) {
            $students[] = $row;
        }
        $resp['success']=true;
        $resp['student']=json_encode($students);
    }
    elseif ($_REQUEST['what'] == "gettwelvedetails") {
        $query = "SELECT
        `id`,
        `twelve_school`,
        `twelve_passyear`,
        `twelve_schooladdress`,
        `twelve_schoolcity`,
        `twelve_schoolpincode`,
        `twelve_specialization`,
        `twelve_board`,
        `twelve_score`,
        `twelve_scoreoutof`,
        `twelve_gapno`,
        `twelve_gapyears`,
        `twelve_admissionquota`
        FROM   `student`
        WHERE `session_id`='$session'";
        if (isset($_REQUEST['id'])) {
            $query .= " AND `user_id`=$_REQUEST[id]";
        }
        $data = $con->query($query);
        $students = array();
        while ($row = $data->fetch_assoc()) {
            $students[] = $row;
        }
        $resp['success']=true;
        $resp['student']=json_encode($students);
    }
    elseif ($_REQUEST['what'] == "getgraduation") {
        $query = "SELECT
        `id`,
        `ug_degree`
        `ug_college`,
        `ug_passyear`,
        `ug_collegeaddress`,
        `ug_collegecity`,
        `ug_collegepincode`,
        `ug_departmentid`,
        `ug_sectorid`,
        `ug_university`,
        `ug_score`,
        `ug_scoreoutof`,
        `ug_gapno`,
        `ug_gapyears`,
        `ug_admissionquota`
        FROM   `student`
        WHERE `session_id`='$session'";
        if (isset($_REQUEST['id'])) {
            $query .= " AND `user_id`=$_REQUEST[id]";
        }
        $data = $con->query($query);
        $students = array();
        while ($row = $data->fetch_assoc()) {
            $students[] = $row;
        }
        $resp['success']=true;
        $resp['student']=json_encode($students);

    }
    elseif ($_REQUEST['what'] == "getpostgraduation") {
        $query = "SELECT
        `id`,
        `pg_degree`,
        `pg_college`,
        `pg_passyear`,
        `pg_collegeaddress`,
        `pg_collegecity`,
        `pg_collegepincode`,
        `pg_departmentid`,
        `pg_sectorid`,
        `pg_university`,
        `pg_score`,
        `pg_scoreoutof`,
        `pg_gapno`,
        `pg_gapyears`,
        `pg_admissionquota`
        FROM   `student`
        WHERE `session_id`='$session'";
        if (isset($_REQUEST['id'])) {
            $query .= " AND `user_id`=$_REQUEST[id]";
        }
        $data = $con->query($query);
        $students = array();
        while ($row = $data->fetch_assoc()) {
            $students[] = $row;
        }
        $resp['success']=true;
        $resp['student']=json_encode($students);
    }

    //All the Get api's should be individual
    //merged with other api's
    /*

    if ($_REQUEST['what'] == "editten_details") {
    }
    if ($_REQUEST['what'] == "studpg_") {
    }
    if ($_REQUEST['what'] == "editpostgraduation") {
    }
    if ($_REQUEST['what'] == "studg") {
    }
    if ($_REQUEST['what'] == "editgraduation") {
    }

    if ($_REQUEST['what'] == "studenttwelve_details") {
    }
    if ($_REQUEST['what'] == "editstudenttwelve_details") {
    }*/

    elseif ($_REQUEST['what'] == "studaddqualication") {
        $cols="";
        $values="";
        $first=false;
        foreach ($_POST as $key => $value) {
            if ($first) {
                $cols+=", `$key`";
                $values+=", '$value'";
            } else {
                $cols+="`$key`";
                $values+="'$value'";
                $first=true;
            }
        }
        if ($first) {
            $query="INSERT INTO `additional_qualifications`($cols) VALUES($values)";
            $result=$con->query($query);
            if ($result>0) {
                $resp['success']=true;
                $resp['message']="Additional qualification successfully added";
            }else {
                $resp['success']=false;
                $resp['message']="There was a problem adding additional qualification please try again";
            }
        }else {
            $resp['success']=false;
            $resp['message']="Please enter atleast one field to update.";
        }
    }
    //get additional qualifications
    elseif ($_REQUEST['what'] == "getadditionalqualification") {
        $query = "SELECT *
        FROM  `additional_qualifications`
        WHERE 1=1";
        if (isset($_REQUEST['id'])) {
            $query .= " AND `id`=$_REQUEST[id]";
        }
        if (isset($_REQUEST['id'])) {
            $query .= " AND `student_id`='$_REQUEST[student_id]'";
        }
        $data = $con->query($query);
        $additional_qualifications = array();
        while ($row = $data->fetch_assoc()) {
            $additional_qualifications[] = $row;
        }
        $resp['success']=true;
        $resp['additional_qulifications']=json_encode($additional_qualifications);
    }

    elseif ($_REQUEST['what'] == "editadditionalqualification") {
    }

    elseif ($_REQUEST['what'] == "studworkexp") {
        $cols="";
        $values="";
        $first=false;
        foreach ($_POST as $key => $value) {
            if ($first) {
                $cols+=", `$key`";
                $values+=", '$value'";
            } else {
                
                $cols+="`$key`";
                $values+="'$value'";
                $first=true;
            }
        }
        if ($first) {
            $query="INSERT INTO `work_experiances`($cols) VALUES($values)";
            $result=$con->query($query);
            if ($result>0) {
                $resp['success']=true;
                $resp['message']="Work experiance successfully added";
            }else {
                $resp['success']=false;
                $resp['message']="There was a problem adding work experiance please try again";
            }
        }else {
            $resp['success']=false;
            $resp['message']="Please enter atleast one field to update.";
        }
    }

    elseif ($_REQUEST['what'] == "getworkexp") {
        $query = "SELECT *
        FROM  `work_experiances`
        WHERE 1=1";
        if (isset($_REQUEST['id'])) {
            $query .= " AND `id`=$_REQUEST[id]";
        }
        if (isset($_REQUEST['id'])) {
            $query .= " AND `student_id`='$_REQUEST[student_id]'";
        }
        $data = $con->query($query);
        $work_experiances = array();
        while ($row = $data->fetch_assoc()) {
            $work_experiances[] = $row;
        }
        $resp['success']=true;
        $resp['work_experiances']=json_encode($work_experiances);

    }

    elseif ($_REQUEST['what'] == "editworkexp") {

    }

    elseif ($_REQUEST['what'] == "studinternship") {
        $cols="";
        $values="";
        $first=false;
        foreach ($_POST as $key => $value) {
            if ($first) {
                $cols+=", `$key`";
                $values+=", '$value'";
            } else {
                
                $cols+="`$key`";
                $values+="'$value'";
                $first=true;
            }
        }
        if ($first) {
            $query="INSERT INTO `internships`($cols) VALUES($values)";
            $result=$con->query($query);
            if ($result>0) {
                $resp['success']=true;
                $resp['message']="Internship successfully added";
            }else {
                $resp['success']=false;
                $resp['message']="There was a problem adding ineternship please try again";
            }
        }else {
            $resp['success']=false;
            $resp['message']="Please enter atleast one field to update.";
            
        }
    }

    elseif ($_REQUEST['what'] == "getinternship") {
        $query = "SELECT *
        FROM  `internships`
        WHERE 1=1";
        if (isset($_REQUEST['id'])) {
            $query .= " AND `id`=$_REQUEST[id]";
        }
        if (isset($_REQUEST['id'])) {
            $query .= " AND `student_id`='$_REQUEST[student_id]'";
        }
        $data = $con->query($query);
        $internships = array();
        while ($row = $data->fetch_assoc()) {
            $internships[] = $row;
        }
        $resp['success']=true;
        $resp['internships']=json_encode($students);
    }

    elseif ($_REQUEST['what'] == "editinternship") {
    }

    //merged with additional qualifications
    // if ($_REQUEST['what'] == "studextraachievement") {
    // }

    // if ($_REQUEST['what'] == "getextraachievement") {
    // }

    // if ($_REQUEST['what'] == "editextraachievement") {
    // }

    //merged in one table of student
    // if ($_REQUEST['what'] == "studentprofile") {
    // }

    // if ($_REQUEST['what'] == "studenteditprofile") {
    // }

    // if ($_REQUEST['what'] == "getstudent") {
    // }

    elseif ($_REQUEST['what'] == "updatepassword") {
        if (isset($_POST['password'])&&isset($_REQUEST['id'])&&$_POST['password']!=""&&$_REQUEST['id']!="") {
            $result=$con->query("UPDATE `users`
                SET `password`='".trim($_POST['password'])."' WHERE `id`='$_REQUEST[id]'");
            if ($result>0) {
                $resp['success']=true;
                $resp['message']="Password updated successfully";
            } else {
                $resp['success']=false;
                $resp['message']="There was a problem updating your password pleast try again.";
            }

        } else {
            $resp['success']=false;
            $resp['message']="To update password id and password are needed";
        }
        
    }

    //Company
    //add company details
    elseif ($_REQUEST['what'] == "companydetails") {
        $cols="";
        $values="";
        $first=false;
        foreach ($_POST as $key => $value) {
            if ($first) {
                $cols+=", `$key`";
                $values+=", '$value'";
            } else {
                $cols+="`$key`";
                $values+="'$value'";
                $first=true;
            }
        }
        if ($first) {
            $query="INSERT INTO `companies`($cols) VALUES($values)";
            $result=$con->query($query);
            if ($result>0) {
                $resp['success']=true;
                $resp['message']="Company successfully added";
            }else {
                $resp['success']=false;
                $resp['message']="There was a problem adding company please try again";
            }
        }else {
            $resp['success']=false;
            $resp['message']="Please enter atleast one field to update.";
        }
        
    }
    //get company details
    elseif ($_REQUEST['what'] == "getcompanydetails") {
        $query = "SELECT * from companies";
        $res = mysqli_query($con,$query);

        $count = mysqli_num_rows($res);

        if ($count>0){
            while($row = mysqli_fetch_assoc($res)){
                $arr[] = $row;
            }
            $resp['result'] = $arr;
        }else{
            $resp['status'] = 'error';
        }
    }
    //merged with getcompany details
    //get company update
    // elseif ($_REQUEST['what'] == "getcompanyupdate") {
    // }

    //Hiring
    //addcompany hiring details
    elseif ($_REQUEST['what'] == "companyhiringdetails") {
        $company_id=$_POST['company_id'];
        $designation=$_POST['designation'];
        $fix_salary=$_POST['fix_salary'];
        $bonus=$_POST['bonus'];
        $performance_inc=$_POST['performance_inc'];
        $total_salary=$_POST['total_salary'];
        $joblocation=$_POST['joblocation'];
        $joindate=$_POST['joindate'];
        $startdate=$_POST['startdate'];
        $enddate=$_POST['enddate'];
        $interview_mode=$_POST['interview_mode'];
        $interview_location=$_POST['interview_location'];
        $other_requirement=$_POST['other_requirement'];
        $query="INSERT INTO `companies`(
                                `company_id`,
                                `designation`,
                                `fix_salary`,
                                `bonus`,
                                `perdormance_inc`,
                                `total_salary`,
                                `joblocation`,
                                `joindate`,
                                `startdate`,
                                `enddate`,
                                `interview_mode`,
                                `interview_location`,
                                `other_requirement`)
                            VALUES(
                                '$company_id',
                                '$designation',
                                '$fix_salary',
                                '$bonus',
                                '$performance_inc',
                                '$total_salary',
                                '$joblocation',
                                '$joindate',
                                '$startdate',
                                '$enddate',
                                '$interview_mode',
                                '$interview_location',
                                '$other_requirement',
                            )";
        $result=$con->query($query);
        if ($result>0) {
            $hiring_id=$con->insert_id;
            $departments=$_POST['departments'];
            $query2="INSERT INTO `hiring_departments` (`hiring_id`,`department_id`) VALUES";
            $firstdepartment=false;
            foreach ($departments as $department) {
                if ($first) {
                    $query2.="('$hiring_id','$department')";
                } else {
                    $query2.=",('$hiring_id','$department')";
                }
            }
            $result2=0;
            if ($firstdepartment) {
                $result2=$con->query($query2);
            }
            $sectors=$_POST['sectors'];
            $query3="INSERT INTO `hiring_sectors` (`hiring_id`,`department_id`) VALUES";
            $firstsector=false;
            foreach ($sectors as $sector) {
                if ($first) {
                    $query2.="('$hiring_id','$sector')";
                } else {
                    $query2.=",('$hiring_id','$sector')";
                }
            }
            $result3=0;
            if ($firstsector) {
                $result3=$con->query($query3);
            }
            if ($result2<0 || $result3<0) {
                $resp['success']=false;
                $resp['message']="Hiring successfully added, Problem adding departments and sectors";
            } else {
                $resp['success']=true;
                $resp['message']="Hiring successfully added";
            }
        }else {
            $resp['success']=false;
            $resp['message']="There was a problem adding hiring please try again";
        }
    }

    //get hiring details
    elseif ($_REQUEST['what'] == "gethiringdetails") {
        
    }
    
    elseif ($_REQUEST['what'] == "getstudenteid") {
        
    }

    elseif ($_REQUEST['what'] == "getstudentdetails") {
    }

    // elseif ($_REQUEST['what'] == "getstudentlogin") {
    // }

    // elseif ($_REQUEST['what'] == "getcoodinatorlogin") {
    // }
    
    elseif ($_REQUEST['what']=="getlogin") {
        if (isset($_POST['email'])&&isset($_POST['password'])) {
            $result=$con->query("SELECT *,`r`.`role` AS `role` ,`u`.`id` AS `id`FROM `users` `u` JOIN `roles` `r`
                    WHERE `r`.`id`=`u`.`role`
                        AND `u`.`email`='$_POST[email]'
                        AND `u`.`password`='$_POST[passsword]'
                        AND `u`.`status`=0
                        AND `r`.`status`=0");
            if ($result->num_rows>0){
               $resp['success']=true;
               $resp['user']=$result->fetch_assoc();
               $role=$resp['user']['role'];
               $id=$resp['user']['id'];
               if ($role=="STUDENT") {
                    $student=$con->query("SELECT * FROM `student` WHERE `user_id`='$id'");
                    if ($student->num_rows>0) {
                        $resp['student']=$student->fetch_assoc();
                    }
               }
            } else {
                $resp['success']=false;
                $resp['message']="Wrong id or password";
            }
        } else {
            $resp['success']=false;
            $resp['message']="Email and password are required";
        }
    }
   

   
    elseif ($_REQUEST['what'] == "getcompanyin") {
    }

    elseif ($_REQUEST['what'] == "getstuddetails") {
    }

    elseif ($_REQUEST['what'] == "getcompanyd") {
    }

    elseif ($_REQUEST['what'] == "getcompanydisplay") {
    }

    

    elseif ($_REQUEST['what'] == "updatecompanydetails") {
    }

    elseif ($_REQUEST['what'] == "updatecompanyhiringdetails") {
    }

    elseif ($_REQUEST['what'] == "studentapply") {
    }

    elseif ($_REQUEST['what'] == "statusupdate") {
    }

    elseif ($_REQUEST['what'] == "applyincompany") {
    }

    elseif ($_REQUEST['what'] == "getapplystudentdetails") {
    }

    elseif ($_REQUEST['what'] == "getstudentapplydetails") {
    }

    elseif ($_REQUEST['what'] == "deleteapply") {
    }
    elseif ($_REQUEST['what'] == "getstudentselection") {
    }
    elseif ($_REQUEST['what'] == "checkmaxcompany") {
    }

    elseif ($_REQUEST['what'] == "insertcsv") {
    }
} else {
    $resp['success'] = false;
    $resp['message'] = "No api link mentioned.";
}

echo json_encode($resp);


?>