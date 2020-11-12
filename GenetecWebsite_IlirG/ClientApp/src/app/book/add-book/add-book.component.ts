import { Component, OnInit } from '@angular/core';
import { BookEntitiesService } from 'src/app/services/entities/book-entities.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css']
})
export class AddBookComponent implements OnInit {
  book = {
    id:0,
    title: '',
    description: '',
    publishedDate: new Date(),
    authors: ''
  };
  submitted = false;
  constructor(private bookEntitiesService: BookEntitiesService, private route: ActivatedRoute,
    private router: Router) {

    let id = this.route.snapshot.paramMap.get("Id");
    if (id != 'null') {
      this.bookEntitiesService.getById(id)
        .subscribe(
          data => {
            if (data.hasError) {
              alert(data.message);
            } else {
              this.book = data.item;
            }
          },
          error => {
            console.log(error);
          });
    }
  }


  ngOnInit() {
   
  }

  saveBook(): void {
    const data = {
      id: this.book.id,
      title: this.book.title,
      description: this.book.description,
      publishedDate: this.book.publishedDate,
      authors: this.book.authors
    };

    this.bookEntitiesService.create(data)
      .subscribe(
        response => {
          console.log(response);
          this.submitted = true;
          alert("Book saved successfully");
          window.location.href = "/book-entities";
        },
        error => {
          console.log(error);
        });
  }


}


