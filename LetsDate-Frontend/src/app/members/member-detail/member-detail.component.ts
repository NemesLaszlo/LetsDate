import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { ToastrService } from 'ngx-toastr';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Message } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';
import { PresenceService } from 'src/app/_services/presence.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  @ViewChild('memberTabs', {static: true}) memberTabs: TabsetComponent;
  activeTab: TabDirective;
  member: Member;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  messages: Message[] = [];
  
  constructor(private memberService: MembersService, private route: ActivatedRoute, private toastr: ToastrService, 
    private messageService: MessageService, public presence: PresenceService) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.member = data.member;
    })

    // Messages tab routes from card button and message component
    this.route.queryParams.subscribe(params => {
      params.tab ? this.selectTab(params.tab) : this.selectTab(0);

    })

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ]
    this.galleryImages = this.getImages();
  }

  selectTab(tabId: number) {
    this.memberTabs.tabs[tabId].active = true;
  }

  loadMessages() {
    this.messageService.getMessageThread(this.member.username).subscribe(messages => {
      this.messages = messages;
    })
  }
  
  getImages(): NgxGalleryImage[] {
    const imageUrls = [];
    for (const photo of this.member.photos) {
      imageUrls.push({
        small: photo?.url,
        medium: photo?.url,
        big: photo?.url
      })
    }
    return imageUrls;
  }

  addLike(member: Member) {
    this.memberService.addLike(member.username).subscribe(() => {
      this.toastr.success('You have liked ' + member.knownAs);
    })
  }

  onTabActivated(data: TabDirective) {
    // craziness, do not know why undefined
    this.activeTab = data;
    if(this.activeTab.heading === undefined) this.activeTab.heading = 'Messages'
    if (this.activeTab.heading === 'Messages' && this.messages.length === 0) {
      this.loadMessages();
    }
  }

}
